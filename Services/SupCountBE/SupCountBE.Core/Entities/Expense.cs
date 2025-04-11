

namespace SupCountBE.Core.Entities
{
    public class Expense

    {
        public  int Id { get; set; }
        public required  string Title { get; set; }
        public float Amount { get; set; }
        public DateTime Dtae { get; set; }
        public required string PayerId { get; set; }
        public User? Payer { get; set; }
        public required string CategoryId { get; set; }
        public ICollection<Participation>? Participations { get; set; }
       
    }
}
