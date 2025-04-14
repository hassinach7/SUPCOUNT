

namespace SupCountBE.Infrastacture.Configurations;

public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
{
    public void Configure(EntityTypeBuilder<Participation> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Weight)
               .IsRequired();

        builder.Property(p => p.UserId)
               .IsRequired();

        builder.Property(p => p.ExpenseId)
               .IsRequired();

        builder.HasOne(p => p.User)
               .WithMany(u => u.Participations)
               .HasForeignKey(p => p.UserId);

        builder.HasOne(p => p.Expense)
               .WithMany(e => e.Participations)
               .HasForeignKey(p => p.ExpenseId);

        builder.ToTable("Participations");
    }
}
