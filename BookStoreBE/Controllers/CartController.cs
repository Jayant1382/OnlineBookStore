using BookStoreBE.Models;
using BookStoreBE.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Transactions;

namespace BookStoreBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        [Route("GetCartItems")]
        public IActionResult GetCartItems(int id)
        {
            try
            {
                var cart = _cartRepository.GetById(id);
                return new OkObjectResult(cart);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while fetching cart items: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("GetOrdersList")]
        public IActionResult GetOrdersList(int id)
        {
            try
            {
                var orders = _cartRepository.OrderList(id);
                return new OkObjectResult(orders);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while fetching orders list: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart([FromBody] Cart cart)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _cartRepository.AddToCart(cart);
                    scope.Complete();
                    return new OkResult();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while adding to cart: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("RemoveFromCart")]
        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _cartRepository.RemoveFromCart(id);
                    scope.Complete();
                    return new OkResult();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while removing from cart: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public IActionResult PlaceOrder(int UserId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _cartRepository.PlaceOrder(UserId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while placing an order: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
