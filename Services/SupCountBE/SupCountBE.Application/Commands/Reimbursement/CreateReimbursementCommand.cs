using SupCountBE.Application.Responses.Reimbursement;

namespace SupCountBE.Application.Commands.Reimbursement;

public class CreateReimbursementCommand : IRequest<ReimbursementResponse>
{
    public string Name { get; set; } = null!;
    //public string SenderId { get; set; } = null!;
    public string BeneficiaryId { get; set; } = null!;
    public float Amount { get; set; }
    public int GroupId { get; set; }
}
