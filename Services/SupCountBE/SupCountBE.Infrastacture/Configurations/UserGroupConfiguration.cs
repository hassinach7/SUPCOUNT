

namespace SupCountBE.Infrastacture.Configurations;

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        //builder.HasKey(ug => ug.Id);
        builder.HasKey(ug => new { ug.UserId, ug.GroupId });

        builder.Property(ug => ug.Role)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(ug => ug.CreatedAt)
               .IsRequired();

        builder.HasOne(ug => ug.User)
               .WithMany(u => u.UserGroups)
               .HasForeignKey(ug => ug.UserId);

        builder.HasOne(ug => ug.Group)
               .WithMany(g => g.UserGroups)
               .HasForeignKey(ug => ug.GroupId);

        builder.ToTable("UserGroups");
    }
}
