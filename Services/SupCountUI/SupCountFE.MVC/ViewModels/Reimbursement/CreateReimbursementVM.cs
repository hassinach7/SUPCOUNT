using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SupCountFE.MVC.ViewModels.Reimbursement
{
    public class CreateReimbursementVM
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string BeneficiaryId { get; set; } = null!;

        [Required]
        public float Amount { get; set; }

        [Required]
        public int GroupId { get; set; }

        // Dropdown sources
        public SelectList? BeneficiariesItems { get; set; }
        public SelectList? GroupsItems { get; set; } 
    }
}
