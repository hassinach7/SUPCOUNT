using SupCountBE.Application.Responses.Justification;
using SupCountBE.Core.Enums;

namespace SupCountBE.Application.Commands.Justification;

public class CreateJustificationCommand : IRequest<JustificationResponse>
{
    public int? ExpenseId { get; set; }
    public byte[] FileContent { get; set; } = null!;
    public JustificationTypeEnum Type { get; set; } = JustificationTypeEnum.Transaction;
}
