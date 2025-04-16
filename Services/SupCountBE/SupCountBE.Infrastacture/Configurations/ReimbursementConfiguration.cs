
namespace SupCountBE.Infrastacture.Configurations;

public class ReimbursementConfiguration : IEntityTypeConfiguration<Reimbursement>
{
    public void Configure(EntityTypeBuilder<Reimbursement> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(r => r.SenderId)
               .IsRequired();

        builder.Property(r => r.BeneficiaryId)
               .IsRequired();

        builder.Property(r => r.GroupId)
               .IsRequired();

        builder.Property(r => r.Amount)
               .IsRequired();

        builder.HasOne(r => r.Sender)
               .WithMany(u => u.ReimbursementsSent)
               .HasForeignKey(r => r.SenderId);


        builder.HasOne(r => r.Beneficiary)
               .WithMany(u => u.ReimbursementsReceived)
               .HasForeignKey(r => r.BeneficiaryId);
               

        builder.HasOne(r => r.Group)
               .WithMany(g => g.Reimbursements)
               .HasForeignKey(r => r.GroupId);

        builder.HasMany(r => r.Transactions)
               .WithOne(t => t.Reimbursement)
               .HasForeignKey(t => t.ReimbursementId);

        builder.ToTable("Reimbursements");
    }
}
