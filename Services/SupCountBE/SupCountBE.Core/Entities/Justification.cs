

namespace SupCountBE.Core.Entities
{
    public  class Justification

    {
        public int Id   { get; set; }
        public int ExpenseId { get; set; }
        public required string FilePath { get; set; }
        public required string Type { get; set; }
    }
}
