namespace BookStoreBE.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string OrderNo { get; set; }      
        public decimal OrderTotal { get; set; }        
        public DateTime CreatedOn { get; set; }

        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
