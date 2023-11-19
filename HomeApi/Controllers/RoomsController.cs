using AutoMapper;
using HomeApi.Contracts.Models.Room;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
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
            var room = await _roomRepository.GetRoomByName(request.Name);
            if (room != null)
            {
                return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
            }
            var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
            await _roomRepository.AddRoom(newRoom);

            return StatusCode(201, $"Комната {request.Name} добавлена!");
        }

        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> Edit(
            [FromRoute] string name,
            [FromBody] EditeRoomRequest request)
        {
            var room = await _roomRepository.GetRoomByName(name);
            if (room == null)
            {
                return StatusCode(400, $"Ошибка: Комната \"{name}\" не существует.");
            }
            var query = _mapper.Map<EditeRoomRequest, UpdateRoomQuery>(request);
            await _roomRepository.UpdateRoom(room, query);

            return StatusCode(200, $"Комната {room.Name} уже не будет прежней!");
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
