
using SupCountBE.Infrastacture.Configurations;
using SupCountBE.Infrastacture.Extentions;

namespace SupCountBE.Infrastacture.Data.Context;

public class SupCountDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<User> Users { get; set; }
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

        
        modelBuilder.InitiData();
    }
}
