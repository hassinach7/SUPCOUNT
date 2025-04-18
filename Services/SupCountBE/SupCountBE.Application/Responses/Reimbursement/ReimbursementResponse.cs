namespace SupCountBE.Application.Responses;

public class ReimbursementResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public float Amount { get; set; }
    public string SenderId { get; set; } = null!;
    public string BeneficiaryId { get; set; } = null!;
    public int GroupId { get; set; }
}
