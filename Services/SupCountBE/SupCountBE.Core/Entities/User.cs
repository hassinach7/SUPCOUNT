﻿using Microsoft.AspNetCore.Identity;

namespace SupCountBE.Core.Entities;
public class User : IdentityUser
{
    public float Balance { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public required  string FullName {  get; set; }
    public ICollection<Expense>? Expenses { get; set; } 
    public ICollection<Participation>? Participations { get; set; }
    public ICollection<Reimbursement>? ReimbursementsSent { get; set; }
    public ICollection<Reimbursement>? ReimbursementsReceived { get; set; }

    public ICollection<Message>? SentMessages { get; set; }
    public ICollection<Message>? ReceivedMessages { get; set; }
    public ICollection<UserGroup>? UserGroups { get; set; }
}
