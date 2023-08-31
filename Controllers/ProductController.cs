using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        // Создание нового запроса
        [HttpGet]
        public List<Product> GetProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }

        // Запрос на добавление новой корзины
        [HttpPost]
        public List<Product> PostProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        // Запрос на удаление корзины с ответом назад
        [HttpDelete("/kustutaProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }


        // Запрос на получение записи из базы данных
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // Запрос на изменение записи с помощью PUT запроса
        [HttpPut("{id}")]
        public ActionResult<List<Product>> PutProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Image= updatedProduct.Image;
            product.Active= updatedProduct.Active;
            product.Stock= updatedProduct.Stock;
            


            _context.Products.Update(product);
            _context.SaveChanges();

            return Ok(_context.Products);
        }



    }
}

