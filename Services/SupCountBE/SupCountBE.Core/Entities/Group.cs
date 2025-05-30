﻿

namespace SupCountBE.Core.Entities;

public class Group : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<UserGroup>? UserGroups { get; set; }
    public ICollection<Expense>? Expenses { get; set; }
    public ICollection<Reimbursement>? Reimbursements { get; set; }
    public ICollection<Message>? Messages { get; set; }
}
