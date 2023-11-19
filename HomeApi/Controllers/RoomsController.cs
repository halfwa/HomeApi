using AutoMapper;
using HomeApi.Contracts.Models.Room;
using HomeApi.Data.Models;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController: ControllerBase
    {
        private IRoomRepository _roomRepository;
        private IMapper _mapper;
        public RoomsController(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение списка комнат
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomRepository.GetRooms();
            if (rooms == null) { return StatusCode(200, "Мдааа"); }

            var response = new GetRoomsResponse()
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };

            return StatusCode(200, response);
        }

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(
            [FromBody] AddRoomRequest request)
        {
            var existingRoom = await _roomRepository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _roomRepository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }

            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> Delete([FromRoute] string name)
        {
            var room = await _roomRepository.GetRoomByName(name);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комнаты \"{name}\" не существует.");

            await _roomRepository.DeleteRoom(room);

            return StatusCode(200, $"Устройство {name} успешно удалено!");
        }


    }
}
