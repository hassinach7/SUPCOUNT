using SupCountBE.Application.Responses.Reimbursement;

namespace SupCountBE.Application.Queries.Reimbursement
{
    public class GetReimbursementByIdQuery(int id) : IRequest<ReimbursementResponse>
    {
        public int Id { get; set; } = id;
    }
}
