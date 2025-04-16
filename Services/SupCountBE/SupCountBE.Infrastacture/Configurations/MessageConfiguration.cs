

namespace SupCountBE.Infrastacture.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Content)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(m => m.SenderId)
               .IsRequired();

        builder.Property(m => m.RecipientId)
               .IsRequired(false);

        builder.HasOne(m => m.Sender)
               .WithMany(u => u.SentMessages)
               .HasForeignKey(m => m.SenderId);

        builder.HasOne(m => m.Recipient)
               .WithMany(u => u.ReceivedMessages)
               .HasForeignKey(m => m.RecipientId);

        builder.HasOne(m => m.Group)
               .WithMany(g => g.Messages)
               .HasForeignKey(m => m.GroupId);

        builder.ToTable("Messages");
    }
}
