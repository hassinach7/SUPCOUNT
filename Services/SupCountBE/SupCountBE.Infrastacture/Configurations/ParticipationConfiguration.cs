

namespace SupCountBE.Infrastacture.Configurations;

public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
{
    public void Configure(EntityTypeBuilder<Participation> builder)
    {
        //builder.HasKey(p => new { p.UserId, p.ExpenseId });

        builder.Property(p => p.Weight)
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
