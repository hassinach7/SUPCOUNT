using SupCountBE.Application.Responses.Reimbursement;

namespace SupCountBE.Application.Queries.Reimbursement
{
    public class GenerateReimbursementsQuery(int groupId) : IRequest<List<ReimbursementResponse>>
    {
        public int GroupId { get; set; } = groupId;
    }
   
}
