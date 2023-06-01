using BookStoreBE.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreBE.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MyDbContext _dbContext;
        public CartRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddToCart(Cart cart)
        {
            _dbContext.cart.Add(cart);
            Save();
        }
        public IEnumerable<CartItems> GetById(int UserId)
        {
            var books = _dbContext.books.ToList();
            var cartItems = _dbContext.cart.Where(x => x.UserId == UserId).ToList();
            var result = cartItems.Join(books,
                cart => cart.BookId,
                book => book.ID, (cart, book) => new CartItems
                {
                    Name = book.Name,
                    UnitPrice = book.UnitPrice,
                    Author = book.Author,
                    CartId = cart.ID,
                    BookId = book.ID,
                    UserId = cart.UserId,
                    Quantity = cart.Quantity
                });
            return result;
        }
        public IEnumerable<Orders> OrderList(int UserId)
        {
            return _dbContext.orders.Where(x => x.UserId == UserId).ToList();
        }

        public void PlaceOrder(int UserId)
        {
            var CartList = _dbContext.cart.Where(x => x.UserId == UserId).ToList();
            if (CartList != null)
            {
                var OrderNumber = Guid.NewGuid().ToString();
                Orders order = new Orders();
                order.UserId = UserId;
                order.OrderNo = OrderNumber.ToString();
                order.OrderTotal = CartList.Select(x => x.UnitPrice * x.Quantity).Sum();
                order.CreatedOn = DateTime.Now.Date;
                _dbContext.orders.Add(order);
                Save();

                var orderId = _dbContext.orders.FirstOrDefault(x => x.OrderNo == OrderNumber.ToString()).ID;
                if (orderId > 0)
                {
                    for (int i = 0; i < CartList.Count; i++)
                    {
                        OrderItems orderItems = new OrderItems();
                        orderItems.OrderID = orderId;
                        orderItems.BookID = CartList[i].BookId;
                        orderItems.Quantity = CartList[i].Quantity;
                        orderItems.UnitPrice = CartList[i].UnitPrice;
                        orderItems.TotalPrice = CartList[i].Quantity * CartList[i].UnitPrice;
                        _dbContext.orderItems.Add(orderItems);
                        Save();
                    }
                }
                var cartOrderId = _dbContext.orderItems.FirstOrDefault(x => x.OrderID == orderId).ID;
                if(orderId > 0 && cartOrderId > 0)
                {
                    for (int i = 0; i < CartList.Count; i++)
                    {
                        _dbContext.cart.Remove(CartList[i]);
                        Save();
                    }
                }
            }
        }

        public void RemoveFromCart(int id)
        {
            Cart cart = _dbContext.cart.Find(id);
            if (cart != null)
            {
                _dbContext.cart.Remove(cart);
                Save();
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
