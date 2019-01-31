using System.Collections.Generic;
using System.Linq;
using AsrWebApi.Data;
using AsrWebApi.Models.Repository;

namespace AsrWebApi.Models.DataManager
{
    public class SlotManager : IDataRepository<Slot, KeyPair>
    {
        private readonly AsrContext _context;

        public SlotManager(AsrContext context)
        {
            _context = context;
        }

        public Slot Get(KeyPair pair)
        {
            return _context.Slot.FirstOrDefault(
                x => x.RoomID == pair.RoomID && x.StartTime == pair.StartTime);
        }

        public IEnumerable<Slot> GetAll()
        {
            return _context.Slot.ToList();
        }

        public KeyPair Add(Slot slot)
        {
            _context.Slot.Add(slot);
            _context.SaveChanges();

            return new KeyPair(slot.RoomID, slot.StartTime);
        }

        public KeyPair Delete(KeyPair pair)
        {
            _context.Slot.Remove(_context.Slot.FirstOrDefault(
                x => x.RoomID == pair.RoomID && x.StartTime == pair.StartTime));
            _context.SaveChanges();

            return pair;
        }

        public KeyPair Update(KeyPair pair, Slot slot)
        {
            _context.Update(slot);
            _context.SaveChanges();

            return pair;
        }
    }
}
