using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asr.Data;
using Asr.Models;

namespace Asr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly AsrContext _context;

        public SlotsController(AsrContext context)
        {
            _context = context;
        }

        // GET: api/Slots
        [HttpGet]
        public IEnumerable<Slot> GetSlot()
        {
            return _context.Slot;
        }

        // GET: api/Slots/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSlot([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var slot = await _context.Slot.FindAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return Ok(slot);
        }

        // PUT: api/Slots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlot([FromRoute] string id, [FromBody] Slot slot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != slot.RoomID)
            {
                return BadRequest();
            }

            _context.Entry(slot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Slots
        [HttpPost]
        public async Task<IActionResult> PostSlot([FromBody] Slot slot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Slot.Add(slot);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SlotExists(slot.RoomID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSlot", new { id = slot.RoomID }, slot);
        }

        // DELETE: api/Slots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var slot = await _context.Slot.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            _context.Slot.Remove(slot);
            await _context.SaveChangesAsync();

            return Ok(slot);
        }

        private bool SlotExists(string id)
        {
            return _context.Slot.Any(e => e.RoomID == id);
        }
    }
}