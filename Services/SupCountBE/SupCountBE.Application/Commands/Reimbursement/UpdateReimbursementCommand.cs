using MediatR;
using SupCountBE.Application.Responses.Reimbursement;

namespace SupCountBE.Application.Commands.Reimbursement;

public class UpdateReimbursementCommand : IRequest<ReimbursementResponse>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public float Amount { get; set; }
}
