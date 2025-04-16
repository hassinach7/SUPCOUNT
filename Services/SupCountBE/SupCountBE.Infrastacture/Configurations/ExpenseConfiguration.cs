

namespace SupCountBE.Infrastacture.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(e => e.Amount)
               .IsRequired();

        builder.Property(e => e.Date)
               .IsRequired();

        builder.Property(e => e.PayerId)
               .IsRequired();

        builder.Property(e => e.CategoryId)
               .IsRequired();

        builder.Property(e => e.GroupId)
               .IsRequired();

        builder.HasOne(e => e.Payer)
               .WithMany(u => u.Expenses)
               .HasForeignKey(e => e.PayerId);

        builder.HasOne(e => e.Category)
               .WithMany(c => c.Expenses)
               .HasForeignKey(e => e.CategoryId);

        builder.HasOne(e => e.Group)
               .WithMany(g => g.Expenses)
               .HasForeignKey(e => e.GroupId);

        builder.HasMany(e => e.Participations)
               .WithOne(p => p.Expense)
               .HasForeignKey(p => p.ExpenseId);

        builder.HasMany(e => e.Justifications)
               .WithOne(j => j.Expense)
               .HasForeignKey(j => j.ExpenseId);

        builder.ToTable("Expenses");
    }
}
