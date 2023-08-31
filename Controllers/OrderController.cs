using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Создание нового запроса
        [HttpGet]
        public List<Order> GetOrder()
        {
            var orders = _context.Orders.ToList();
            return orders;
        }

        // Запрос на добавление новой корзины
        [HttpPost]
        public List<Order> PostCartProduct([FromBody] Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }

        // Запрос на удаление корзины с ответом назад
        [HttpDelete("/kustutaOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }


        // Запрос на получение записи из базы данных
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // Запрос на изменение записи с помощью PUT запроса
        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updatedOrder)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            order.created= updatedOrder.created;
            order.TotalSum = updatedOrder.TotalSum;
            order.Paid = updatedOrder.Paid;
            order.Person= updatedOrder.Person;

            _context.Orders.Update(order);
            _context.SaveChanges();

            return Ok(_context.Orders);
        }


    }
}
