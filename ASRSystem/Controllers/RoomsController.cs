using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asr.Data;
using Asr.Models;
using Asr.Models.DataManager;

namespace Asr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("{id}")]
        public Room Get(string RoomID)
        {
            return _repo.Get(RoomID);
        }

        // PUT: api/Rooms
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
        public string Delete(string RoomID)
        {
            return _repo.Delete(RoomID);
        }
    }
}