using Microsoft.AspNetCore.Mvc.Rendering;

namespace SupCountFE.MVC.ViewModels.Participation
{
    public class CreateParticipationVM
    {
        public int? ExpenseId { get; set; }
        public float Weight { get; set; }
        public SelectList? ExpensesItems { get; set; }
    }
}