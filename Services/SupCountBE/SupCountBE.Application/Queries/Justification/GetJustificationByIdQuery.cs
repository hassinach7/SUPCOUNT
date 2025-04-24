using SupCountBE.Application.Responses.Justification;

namespace SupCountBE.Application.Queries.Justification
{
    public class GetJustificationByIdQuery(int id) : IRequest<JustificationResponse>
    {
        public int Id { get; set; } = id;
    }
}
