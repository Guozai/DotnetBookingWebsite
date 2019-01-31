using System.Collections.Generic;
using System.Linq;
using Asr.Data;
using Asr.Models.Repository;

namespace Asr.Models.DataManager
{
    public class RoomManager : IDataRepository<Room, string>
    {
        private readonly AsrContext _context;

        public RoomManager(AsrContext context)
        {
            _context = context;
        }

        public Room Get(string RoomID)
        {
            return _context.Room.FirstOrDefault(x => x.RoomID == RoomID);
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Room.ToList();
        }

        public string Add(Room room)
        {
            _context.Room.Add(room);
            _context.SaveChanges();

            return room.RoomID;
        }

        public string Delete(string RoomID)
        {
            _context.Room.Remove(_context.Room.FirstOrDefault(x => x.RoomID == RoomID));
            _context.SaveChanges();

            return RoomID;
        }

        public string Update(string RoomID, Room room)
        {
            _context.Update(room);
            _context.SaveChanges();

            return RoomID;
        }
    }
}
