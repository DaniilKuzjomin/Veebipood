using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        // Создание нового запроса
        [HttpGet]
        public List<CartProduct> GetCartProducts()
        {
            var cartproducts = _context.CartProducts.ToList();
            return cartproducts;
        }

        // Запрос на добавление новой корзины
        [HttpPost]
        public List<CartProduct> PostCartProduct([FromBody] CartProduct cartproduct)
        {
            _context.CartProducts.Add(cartproduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        // Запрос на удаление корзины с ответом назад
        [HttpDelete("/kustutaCartProduct/{id}")]
        public IActionResult DeleteCartProduct(int id)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return NotFound();
            }

            _context.CartProducts.Remove(cartproduct);
            _context.SaveChanges();
            return NoContent();
        }


        // Запрос на получение записи из базы данных
        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProduct(int id)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return NotFound();
            }

            return cartproduct;
        }

        // Запрос на изменение записи с помощью PUT запроса
        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCartProduct(int id, [FromBody] CartProduct updatedCartProduct)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return NotFound();
            }

            cartproduct.Quantity = updatedCartProduct.Quantity;

            _context.CartProducts.Update(cartproduct);
            _context.SaveChanges();

            return Ok(_context.CartProducts);
        }



    }
}
