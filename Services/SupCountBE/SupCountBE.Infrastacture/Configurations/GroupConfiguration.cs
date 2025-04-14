
namespace SupCountBE.Infrastacture.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(g => g.Description)
               .IsRequired()
               .HasMaxLength(300);

        builder.HasMany(g => g.UserGroups)
               .WithOne(ug => ug.Group)
               .HasForeignKey(ug => ug.GroupId);

        builder.HasMany(g => g.Expenses)
               .WithOne(e => e.Group)
               .HasForeignKey(e => e.GroupId);

        builder.HasMany(g => g.Reimbursements)
               .WithOne(r => r.Group)
               .HasForeignKey(r => r.GroupId);

        builder.HasMany(g => g.Messages)
               .WithOne(m => m.Group)
               .HasForeignKey(m => m.GroupId);

        builder.ToTable("Groups");
    }
}
