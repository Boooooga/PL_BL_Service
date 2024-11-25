using Microsoft.AspNetCore.Mvc;
using PL_BL_Service.BL;
using PL_BL_Service.Models;

namespace PL_BL_Service.Controllers
{
    public class BusesController : Controller
    {
        private readonly IBusinessService _businessService;

        // Внедрение зависимости для использования BusinessService
        public BusesController(IBusinessService businessService)
        {
            _businessService = businessService;
        }
        [HttpGet]
        public IActionResult Index()
        {  
            return View();
        }

        // Метод для получения списка автобусов
        [HttpGet("GetAll")]
        public IActionResult GetAllBuses()
        {
            var buses = _businessService.GetAllBuses(); // Вызов метода, который возвращает список автобусов
            return Ok(buses); // Возвращаем данные в формате JSON
        }

        // Метод для получения одного автобуса по id
        [HttpGet("GetBus/{id}")]
        public IActionResult GetBus(int id)
        {
            var bus = _businessService.GetBus(id); // Вызов метода для получения одного автобуса по id
            if (bus == null)
            {
                return NotFound(); // Если автобус не найден, возвращаем 404
            }
            return Ok(bus); // Возвращаем данные автобуса в формате JSON
        }

        // Метод для добавления нового автобуса
        [HttpPost]
        public IActionResult AddBus([FromBody] Bus bus)
        {
            if (bus == null)
            {
                return BadRequest("Данные не были переданы");
            }

            bool isAdded = _businessService.AddBus(bus); // Метод должен принимать объект Bus
            if (!isAdded)
            {
                return BadRequest("Ошибка при добавлении автобуса");
            }

            return CreatedAtAction(nameof(GetBus), new { id = bus.Id }, bus); // Возвращаем статус 201 (Created) с данными добавленного автобуса
        }
    }
}