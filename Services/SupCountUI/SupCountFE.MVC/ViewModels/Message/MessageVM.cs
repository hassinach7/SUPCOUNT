using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Message;

public class MessageVM
{

    [Required(ErrorMessage = "Message content is required.")]
    [Display(Name = "Message")]
    public string NewMessageContent { get; set; } = null!;

    [Required]
    public string SenderId { get; set; } = null!;

    public string? RecipientId { get; set; }
    public int? GroupId { get; set; }


    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public string? SenderName { get; set; }
    public string? RecipientName { get; set; }
    public string? GroupName { get; set; }
    public bool IsPrivate { get; set; } = false;

}





