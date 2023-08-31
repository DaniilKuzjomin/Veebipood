using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Создание нового запроса
        [HttpGet]
        public List<Person> GetPerson()
        {
            var persons = _context.People.ToList();
            return persons;
        }

        // Запрос на добавление новой корзины
        [HttpPost]
        public List<Person> PostPerson([FromBody] Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
            return _context.People.ToList();
        }

        // Запрос на удаление корзины с ответом назад
        [HttpDelete("/kustutaPerson/{id}")]
        public IActionResult DeletePerson(int id)
        {
            var person = _context.People.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            _context.SaveChanges();
            return NoContent();
        }


        // Запрос на получение записи из базы данных
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _context.People.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // Запрос на изменение записи с помощью PUT запроса
        [HttpPut("{id}")]
        public ActionResult<List<Person>> PutPerson(int id, [FromBody] Person updatedPerson)
        {
            var person = _context.People.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            person.PersonCode = updatedPerson.PersonCode;
            person.FirstName= updatedPerson.FirstName;
            person.LastName= updatedPerson.LastName;
            person.Phone= updatedPerson.Phone;
            person.Password= updatedPerson.Password;
            person.Admin= updatedPerson.Admin;

            _context.People.Update(person);
            _context.SaveChanges();

            return Ok(_context.People);
        }


    }
}

