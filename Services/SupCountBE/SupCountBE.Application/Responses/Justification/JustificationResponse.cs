

namespace SupCountBE.Application.Responses.Justification
{
    public class JustificationResponse
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string FileBase64 { get; set; } = string.Empty;
    }
}
