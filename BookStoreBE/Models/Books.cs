namespace BookStoreBE.Models
{
    public class Books
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Decimal UnitPrice { get; set; }
        public string Author { get; set; }
        public string CreatedBy { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
