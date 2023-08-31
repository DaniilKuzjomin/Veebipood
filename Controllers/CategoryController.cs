using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }


        // Создание нового запроса
        [HttpGet]
        public List<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        // Запрос на добавление новой корзины
        [HttpPost]
        public List<Category> PostCategory([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }

        // Запрос на удаление корзины с ответом назад
        [HttpDelete("/kustutaCategory/{id}")]
        public IActionResult DeleteCartProduct(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }


        // Запрос на получение записи из базы данных
        [HttpGet("{id}")]
        public ActionResult<Category> Category(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // Запрос на изменение записи с помощью PUT запроса
        [HttpPut("{id}")]
        public ActionResult<List<Category>> PutCategory(int id, [FromBody] Category updatedCategory)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return Ok(_context.Categories);
        }



    }
}

