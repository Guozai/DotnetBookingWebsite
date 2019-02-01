using System;
using System.Collections.Generic;
using System.Linq;
using Asr.Data;
using Asr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asr.Controllers
{
    [Route("api/[controller]")]
    public class SlotsController : Controller
    {
        private readonly AsrContext _context;

        public SlotsController(AsrContext context)
        {
            _context = context;
        }

        // GET: api/slots
        [HttpGet]
        public IEnumerable<Slot> Get()
        {
            // Turn off lazy loading
            _context.ChangeTracker.LazyLoadingEnabled = false;

            return _context.Slot.ToList();
        }

        // GET: api/slots/1
        [HttpGet("{RoomID}/{StartTime}")]
        //[Route("api/slots/{RoomID}/{StartTime}")]
        public Slot Get(string RoomID, DateTime StartTime)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            return _context.Slot.FirstOrDefault(
                x => x.RoomID == RoomID && x.StartTime == StartTime);
        }

        // PUT: api/slots
        [HttpPost]
        public void Post([FromBody] Slot slot)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            _context.Add(slot);
        }

        // POST: api/slots
        [HttpPut("{RoomID}/{StartTime}")]
        public void Put(string RoomID, DateTime StartTime, [FromBody] Slot slot)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            _context.Update(slot);
            _context.SaveChanges();
        }

        // DELETE: api/slots/1
        [HttpDelete("{RoomID}/{StartTime}")]
        public void Delete(string RoomID, DateTime StartTime)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            _context.Slot.Remove(_context.Slot.FirstOrDefault(
                x => x.RoomID == RoomID && x.StartTime == StartTime));
            _context.SaveChanges();
        }
    }
}