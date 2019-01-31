using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asr.Models
{
    public class StaffCreateViewModel
    {
        [Required]
        public SelectList RoomIDs { get; set; }

        public string RoomID { get; set;
        }
        [Required]
        public DateTime StartTime { get; set; }
    }
}
