using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AsrWebApi.Models;
using AsrWebApi.Models.DataManager;

namespace Asr.Controllers
{
    [Route("api/[controller]")]
    public class SlotsController : Controller
    {
        private readonly SlotManager _repo;

        public SlotsController(SlotManager repo)
        {
            _repo = repo;
        }

        // GET: api/slots
        [HttpGet]
        public IEnumerable<Slot> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/slots/1
        [HttpGet("{RoomID}")]
        public Slot Get(KeyPair pair)
        {
            return _repo.Get(pair);
        }

        // PUT: api/slots
        [HttpPut("{RoomID}")]
        public void Put([FromBody] Slot slot)
        {
            _repo.Add(slot);
        }

        // POST: api/slots
        [HttpPost]
        public void Post(KeyPair pair, [FromBody] Slot slot)
        {
            _repo.Update(pair, slot);
        }

        // DELETE: api/slots/1
        [HttpDelete("{RoomID}")]
        public KeyPair Delete(KeyPair pair)
        {
            return _repo.Delete(pair);
        }
    }
}