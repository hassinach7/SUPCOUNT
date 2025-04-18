

namespace SupCountBE.Application.Responses.Justification
{
    public class JustificationResponse
    {
        public int Id { get; set; }
        public string ExpenseTitle { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string FileContent { get; set; } = null!;
    }
}
