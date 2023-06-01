using BookStoreBE.Models;

namespace BookStoreBE.Repository
{
    public interface ICartRepository
    {
        IEnumerable<CartItems> GetById(int UserId);
        void AddToCart(Cart cart);
        void RemoveFromCart(int id);
        void PlaceOrder(int UserId);
        IEnumerable<Orders> OrderList(int UserId);
        void Save();
    }
}
