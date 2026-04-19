using System.Runtime.InteropServices.JavaScript;
using apbd_tut6.DTOs;
using apbd_tut6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tut6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get(DateTime? date, string? status, int? roomId)
        {
            
            var query = DataStore.Reservations.AsQueryable();
            if (date is not null)
            {
                query = query.Where(r => r.date == date.Value);
            }

            if (status is not null)
            {
                query = query.Where(r => r.status == status);
            }

            if (roomId is not null)
            {
                query = query.Where(r => r.roomId == roomId.Value);
            }

            return Ok(query.ToList());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateReservationDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            
            var room = DataStore.Rooms.FirstOrDefault(r => r.id == dto.roomId);
            if (room == null)
            {
                return NotFound("Room does not exist");
            }

            if (!room.isActive)
            {
                return NotFound("Room not active");
            }
            
            

            int id = DataStore.Reservations.Max(r => r.id) + 1;
            
            var exisRes = DataStore.Reservations.Where(r => r.roomId == dto.roomId);

            if (exisRes.Any(r => r.endTime > dto.startTime && dto.endTime > r.startTime && r.date == dto.date))
            {
                return Conflict("Overlapping reservations");
            }
            
            if (dto.endTime <= dto.startTime)
            {
                return BadRequest();
            }

            var reservation = new Reservation
            {
                id = id,
                roomId = dto.roomId,
                organizerName =  dto.organizerName,
                topic =  dto.topic,
                date  = dto.date,
                startTime = dto.startTime,
                endTime = dto.endTime,
                status = dto.status,
            };

            DataStore.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.id }, reservation);
        }
        
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateReservationDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var room = DataStore.Rooms.FirstOrDefault(r => r.id == dto.roomId);
            if (room == null)
            {
                return NotFound("Room does not exist");
            }

            if (!room.isActive)
            {
                return NotFound("Room not active");
            }
            
            var exisRes = DataStore.Reservations.Where(r => r.roomId == dto.roomId && r.id != id);

            if (exisRes.Any(r => r.endTime > dto.startTime && dto.endTime > r.startTime && r.date == dto.date))
            {
                return Conflict("Overlapping reservations");
            }
            
            if (dto.endTime <= dto.startTime)
            {
                return BadRequest();
            }
            
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.roomId =  dto.roomId;
            reservation.organizerName = dto.organizerName;
            reservation.topic = dto.topic;
            reservation.date = dto.date;
            reservation.startTime = dto.startTime;
            reservation.endTime = dto.endTime;
            reservation.status = dto.status;

            return Ok(reservation);


        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.id == id);

            if (reservation == null)
            {
                return NotFound();
            }
            
            DataStore.Reservations.Remove(reservation);
            
            return NoContent();
        }
        
        
    }
}
