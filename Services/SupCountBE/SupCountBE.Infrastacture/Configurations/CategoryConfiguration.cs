
namespace SupCountBE.Infrastacture.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.HasMany(c => c.Expenses).WithOne(e => e.Category).HasForeignKey(e => e.CategoryId);

        builder.ToTable("Categories");
    }
}
