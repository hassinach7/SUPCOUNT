
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SupCountBE.Infrastacture.Configurations;

namespace SupCountBE.Infrastacture.Data.Context;

public class SupCountDbContext : IdentityDbContext<User,ApplicationRole, string>
{
    public string? UserId { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Reimbursement> Reimbursements { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Justification> Justifications { get; set; }

    public SupCountDbContext(DbContextOptions<SupCountDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new JustificationConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipationConfiguration());
        modelBuilder.ApplyConfiguration(new ReimbursementConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new UserGroupConfiguration());


        //modelBuilder.InitiData();
        // DeleteBehiavor configuration for all entities 
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(o => o.GetForeignKeys()))
        {
           foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
    }
}
