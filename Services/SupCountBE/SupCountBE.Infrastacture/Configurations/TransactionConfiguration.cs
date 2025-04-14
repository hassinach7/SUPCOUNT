
namespace SupCountBE.Infrastacture.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.ReimbursementId)
               .IsRequired();

        builder.Property(t => t.PaymentMethod)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Amount)
               .IsRequired();

        builder.HasOne(t => t.Reimbursement)
               .WithMany(r => r.Transactions)
               .HasForeignKey(t => t.ReimbursementId);

        builder.ToTable("Transactions");
    }
}
