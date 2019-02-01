using System.Collections.Generic;
using System.Linq;
using Asr.Data;
using Asr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asr.Controllers
{
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly AsrContext _context;

        public RoomsController(AsrContext context)
        {
            _context = context;
        }

        // GET: api/rooms
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            // Turn off lazy loading
            _context.ChangeTracker.LazyLoadingEnabled = false;

            return _context.Room.ToList();
        }

        // GET: api/rooms/A
        [HttpGet("{RoomID}")]
        public Room Get(string RoomID)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            return _context.Room.FirstOrDefault(x => x.RoomID == RoomID);
        }

        // PUT: api/rooms
        [HttpPut("{RoomID}")]
        public void Put([FromBody] Room room)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            _context.Room.Add(room);
            _context.SaveChanges();
        }

        // POST: api/rooms
        [HttpPost]
        public void Post([FromBody] Room room)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            _context.Update(room);
            _context.SaveChanges();
        }

        // DELETE: api/rooms/1
        [HttpDelete("{RoomID}")]
        public string Delete(string RoomID)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            _context.Room.Remove(_context.Room.FirstOrDefault(x => x.RoomID == RoomID));
            _context.SaveChanges();

            return RoomID;
        }
    }
}