using SupCountBE.Application.Queries.Reimbursement;
using SupCountBE.Application.Responses.Reimbursement;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Reimbursement
{
    public class GenerateReimbursementsHandler : IRequestHandler<GenerateReimbursementsQuery, List<ReimbursementResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IReimbursementRepository _reimbursementRepository;
        private readonly IMapper _mapper;

        public GenerateReimbursementsHandler(
            IExpenseRepository expenseRepository,
            IReimbursementRepository reimbursementRepository,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _reimbursementRepository = reimbursementRepository;
            _mapper = mapper;
        }

        public async Task<List<ReimbursementResponse>> Handle(GenerateReimbursementsQuery request, CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepository.GetByGroupIdWithParticipationsAsync(request.GroupId);

            if (expenses == null || !expenses.Any())
                throw new ArgumentException("No expenses found for this group.");

            var balances = new Dictionary<string, float>();

            // 1. Calcul des soldes nets
            foreach (var expense in expenses)
            {
                if (expense.Participations == null || expense.Participations.Count == 0)
                    continue;

                float perPerson = (float)(expense.Amount / expense.Participations.Count);

                foreach (var participation in expense.Participations)
                {
                    var participantId = participation.UserId;
                    balances.TryAdd(participantId, 0);
                    balances[participantId] -= perPerson;
                }

                balances.TryAdd(expense.PayerId, 0);
                balances[expense.PayerId] += (float)expense.Amount;
            }

            // Arrondir les soldes à 2 décimales
            foreach (var key in balances.Keys.ToList())
            {
                balances[key] = (float)Math.Round(balances[key], 2);
            }

            var reimbursements = new List<M.Reimbursement>();

            // 2. Algorithme de min cash flow
            while (balances.Any(b => Math.Abs(b.Value) > 0.01))
            {
                var debtor = balances.OrderBy(b => b.Value).First();
                var creditor = balances.OrderByDescending(b => b.Value).First();

                float amount = Math.Min(-debtor.Value, creditor.Value);
                amount = (float)Math.Round(amount, 2);

                if (amount <= 0.01f)
                    break;

                reimbursements.Add(new M.Reimbursement
                {
                    Name = "Optimized auto reimbursement",
                    SenderId = debtor.Key,
                    BeneficiaryId = creditor.Key,
                    Amount = amount,
                    GroupId = request.GroupId
                });

                balances[debtor.Key] += amount;
                balances[creditor.Key] -= amount;
            }

            if (reimbursements.Count == 0)
                throw new InvalidOperationException("No reimbursements to generate: balances are already settled.");

            foreach (var reimbursement in reimbursements)
            {
                await _reimbursementRepository.AddAsync(reimbursement);
            }

            // 3. Mapping des remboursements complets
            var responses = new List<ReimbursementResponse>();
            foreach (var reimbursement in reimbursements)
            {
                var full = await _reimbursementRepository.GetByIdIncludingAsync(
                    reimbursement.Id,
                    new ReimbursementIncludingProperties
                    {
                        IncludeSenders = true,
                        IncludeBeneficiaries = true,
                        IncludeGroups = true,
                        IncludeTransactions = false
                    });

                responses.Add(_mapper.Map<ReimbursementResponse>(full));
            }

            return responses;
        }
    }
}
