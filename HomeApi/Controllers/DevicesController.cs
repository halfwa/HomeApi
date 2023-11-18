using HomeApi.Confugurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController: ControllerBase
    {
        private readonly IOptions<HomeOptions> _options;
        private readonly IHostEnvironment _env;
        public DevicesController(IOptions<HomeOptions> options, IHostEnvironment env)
        {
            _options = options;
            _env = env;
        }

        /// <summary>
        /// Поиск и загрузка инструкции по использованию устройства 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        }
    }
}
