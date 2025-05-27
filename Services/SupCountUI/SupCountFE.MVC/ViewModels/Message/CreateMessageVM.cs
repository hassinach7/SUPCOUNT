using Microsoft.AspNetCore.Mvc.Rendering;
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
        public bool? IsPrivate { get; set; } = false;

        public SelectList? UsersItems { get; set; }
        public SelectList? GroupsItems { get; set; }
        public List<MessageVM> Messages { get; set; } = new List<MessageVM>();
    }
}
