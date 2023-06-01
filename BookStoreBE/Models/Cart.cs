namespace BookStoreBE.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
