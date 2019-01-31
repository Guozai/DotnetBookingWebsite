using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AsrWebApi.Models;
using AsrWebApi.Models.DataManager;

namespace Asr.Controllers
{
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly RoomManager _repo;

        public RoomsController(RoomManager repo)
        {
            _repo = repo;
        }

        // GET: api/Rooms
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/Rooms/1
        [HttpGet("{RoomID}")]
        public Room Get(string RoomID)
        {
            return _repo.Get(RoomID);
        }

        // PUT: api/Rooms
        [HttpPut("{RoomID}")]
        public void Put([FromBody] Room room)
        {
            _repo.Add(room);
        }

        // POST: api/Rooms
        [HttpPost]
        public void Post([FromBody] Room room)
        {
            _repo.Update(room.RoomID, room);
        }

        // DELETE: api/Rooms/1
        [HttpDelete("{RoomID}")]
        public string Delete(string RoomID)
        {
            return _repo.Delete(RoomID);
        }
    }
}