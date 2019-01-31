using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Asr.Models
{
    public class StaffCreateViewModel
    {
        [Required]
        public List<string> RoomIDs { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
    }
}
