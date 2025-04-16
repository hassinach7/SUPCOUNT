
namespace SupCountBE.Infrastacture.Configurations;

public class JustificationConfiguration : IEntityTypeConfiguration<Justification>
{
    public void Configure(EntityTypeBuilder<Justification> builder)
    {
        builder.HasKey(j => j.Id);

        builder.Property(j => j.FileContent)
               .IsRequired();

        builder.Property(j => j.Type)
               .IsRequired();

        builder.Property(j => j.ExpenseId)
               .IsRequired();

        builder.HasOne(j => j.Expense)
               .WithMany(e => e.Justifications)
               .HasForeignKey(j => j.ExpenseId);

        builder.ToTable("Justifications");
    }
}
