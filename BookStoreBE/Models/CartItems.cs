namespace BookStoreBE.Models
{
    public class CartItems
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
