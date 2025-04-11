

namespace SupCountBE.Core.Entities
{
   public  class Participation

    {
        public required string UserId { get; set; }
        public required  int ExpenseId { get; set; }

        public float Weight { get; set; }

        public User? User { get; set; }
        public Expense? Expense { get; set; }

    }
}
