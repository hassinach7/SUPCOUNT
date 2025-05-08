namespace SupCountBE.Application.Responses.User;

public class SoldeUserResponse
{
    public string UserId { get; set; }= string.Empty;
    public string UserFullName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public float UserSolde { get; set; }
}
