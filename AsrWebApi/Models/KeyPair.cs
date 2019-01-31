using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsrWebApi.Models
{
    public class KeyPair
    {
        public string RoomID { get; set; }
        public DateTime StartTime { get; set; }

        public KeyPair(string roomID, DateTime startTime)
        {
            RoomID = roomID;
            StartTime = startTime;
        }
    }
}
