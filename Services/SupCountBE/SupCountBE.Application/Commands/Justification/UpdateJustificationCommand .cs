using SupCountBE.Application.Responses.Justification;
using SupCountBE.Core.Enums;

namespace SupCountBE.Application.Commands.Justification;

public class UpdateJustificationCommand : IRequest<JustificationResponse>
{
    public int Id { get; set; }
    public byte[] FileContent { get; set; } = null!;
    public JustificationTypeEnum Type { get; set; }
}
