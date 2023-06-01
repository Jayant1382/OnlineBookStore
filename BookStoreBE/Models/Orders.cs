namespace BookStoreBE.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string OrderNo { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
