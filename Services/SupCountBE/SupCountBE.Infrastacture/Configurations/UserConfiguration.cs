

namespace SupCountBE.Infrastacture.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(u => u.CreatedAt)
               .IsRequired();

        builder.Property(u => u.UpdatdAt)
               .IsRequired(false);

        builder.HasMany(u => u.Expenses)
               .WithOne(e => e.Payer)
               .HasForeignKey(e => e.PayerId);

        builder.HasMany(u => u.Participations)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId);

        builder.HasMany(u => u.ReimbursementsSent)
               .WithOne(r => r.Sender)
               .HasForeignKey(r => r.SenderId);


        builder.HasMany(u => u.ReimbursementsReceived)
               .WithOne(r => r.Beneficiary)
               .HasForeignKey(r => r.BeneficiaryId);

        builder.HasMany(u => u.SentMessages)
               .WithOne(m => m.Sender)
               .HasForeignKey(m => m.SenderId);

        builder.HasMany(u => u.ReceivedMessages)
               .WithOne(m => m.Recipient)
               .HasForeignKey(m => m.RecipientId);
             
        builder.HasMany(u => u.UserGroups)
               .WithOne(ug => ug.User)
               .HasForeignKey(ug => ug.UserId);

        builder.ToTable("Users");
    }
}
