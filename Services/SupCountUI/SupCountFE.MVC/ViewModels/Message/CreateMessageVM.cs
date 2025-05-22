using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Message
{
    public class CreateMessageVM
    {
        [Required(ErrorMessage = "Message content is required.")]
        public string Content { get; set; } = null!;

        [Required]
        public string SenderId { get; set; } = null!;

        public string? RecipientId { get; set; }

        public int? GroupId { get; set; }
    }
}
