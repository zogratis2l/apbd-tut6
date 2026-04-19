using System.Runtime.ExceptionServices;
using apbd_tut6.DTOs;
using apbd_tut6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tut6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.id == id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetFromBuilding(string buildingCode)
        {
            var rooms = DataStore.Rooms.Where(r => r.buildingCode == buildingCode).ToList();

            if (!rooms.Any())
            {
                return NotFound();
            }

            return Ok(rooms);
        }



        [HttpGet]
        public IActionResult Get(int? minCapacity, bool? hasProjector, bool? activeOnly)
        {
            var query = DataStore.Rooms.AsQueryable();
            if (minCapacity is not null)
            {
                query = query.Where(q => q.capacity >= minCapacity.Value);
            }

            if (hasProjector.HasValue)
            {
                query = query.Where(q => q.hasProjector == hasProjector.Value);
            }

            if (activeOnly.HasValue)
            {
                query = query.Where(q => q.isActive == activeOnly.Value);
            }

            return Ok(query.ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateRoomDto createRoomDto)
        {
            if (createRoomDto == null)
            {
                return BadRequest();
            }

            int id = DataStore.Rooms.Max(r => r.id) + 1;

            var room = new Room
            {
                id = id,
                name = createRoomDto.name,
                buildingCode = createRoomDto.buildingCode,
                floor = createRoomDto.floor,
                capacity = createRoomDto.capacity,
                hasProjector = createRoomDto.hasProjector,
                isActive = createRoomDto.isActive,
            };

            DataStore.Rooms.Add(room);

            return CreatedAtAction(nameof(GetById), new { id = room.id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateRoomDto createRoomDto)
        {
            if (createRoomDto == null)
            {
                return BadRequest();
            }

            var room = DataStore.Rooms.FirstOrDefault(r => r.id == id);
            if (room == null)
            {
                return NotFound();
            }

            room.name = createRoomDto.name;
            room.buildingCode = createRoomDto.buildingCode;
            room.floor = createRoomDto.floor;
            room.capacity = createRoomDto.capacity;
            room.hasProjector = createRoomDto.hasProjector;
            room.isActive = createRoomDto.isActive;

            return Ok(room);


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.id == id);

            if (room == null)
            {
                return NotFound();
            }

            var exisRes = DataStore.Reservations.Where(r => r.roomId == id);

            if (exisRes.Any(r => (r.date + r.endTime) >= DateTime.Now))
            {
                return Conflict("A reservation exists for this room");
            }
            
            DataStore.Rooms.Remove(room);
            
            return NoContent();
        }
        
        
    }
}

