
namespace SupCountBE.Application.Responses.Group;
public class GroupResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string UserGroupsCount { get; set; } = null!;
    public string ExpenseCount { get; set; } = null!;
    public string ReimbursementCount { get; set; } = null!;
    public string MessageCount { get; set; } = null!;

}

    

