namespace SupCountBE.Application.Responses.Participation;

public class ParticipationResponse
{

    public int ExpenseId { get; set; }
    public float Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; } = null!;
    public string ExpenseTitle { get; set; } = null!;
}
