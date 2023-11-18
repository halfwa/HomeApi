using HomeApi.Confugurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using AutoMapper;
using HomeApi.Contracts.Devices;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController: ControllerBase
    {
        private readonly IOptions<HomeOptions> _options;
        private readonly IHostEnvironment _env;
        private readonly IMapper _mapper;
        public DevicesController(
            IOptions<HomeOptions> options, 
            IHostEnvironment env,
            IMapper mapper)
        {
            _options = options;
            _env = env;
            _mapper = mapper;
        }


        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return StatusCode(200, "Устройства отсутствуют");
        }


        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(
            [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            AddDeviceRequest request  // Объект запроса
            )
        {
            return StatusCode(201, $"Устройство {request.Name} добавлено!");
        }

        /// <summary>
        /// Поиск и загрузка инструкции по использованию устройства 
        /// </summary>
        /// <returns></returns>
        /*[HttpGet]
        [HttpHead]
        [Route("{manufacturer}")]
        public IActionResult GetManual([FromRoute] string manufacturer)
        {
            var staticPath = Path.Combine(_env.ContentRootPath, "Static");
            var filePath = Directory.GetFiles(staticPath)
                .FirstOrDefault(f => f.Split("\\")
                .Last()
                .Split(".")[0] == manufacturer);

            if (string.IsNullOrEmpty(filePath))
            {
                return StatusCode(404, $"Инструкции для производителя '{manufacturer}' не найдено на сервере. Пожалуйста проверьте название!");
            }

            string fileType = "application/pdf";
            string fileName = $"{manufacturer}.pdf";

            return PhysicalFile(filePath, fileType, fileName);
        }*/
    }
}
