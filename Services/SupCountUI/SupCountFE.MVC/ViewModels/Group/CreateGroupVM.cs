using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Group;

public class CreateGroupVM


{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
}


