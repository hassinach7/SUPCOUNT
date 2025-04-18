namespace SupCountBE.Application.Responses;

public class ParticipationResponse
{
    public string UserId { get; set; } = null!;
    public int ExpenseId { get; set; }
    public float Weight { get; set; }
}
