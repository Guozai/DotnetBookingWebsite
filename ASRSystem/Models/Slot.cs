// Model copied from Matthew Bolger's model of Week7 example code
// https://stackoverflow.com/questions/16872493/validating-time-only-input-in-asp-net-mvc-unobtrusive-validation

using System;
using System.ComponentModel.DataAnnotations;

namespace Asr.Models
{
    public class Slot
    {
        [Required]
        public string RoomID { get; set; }
        public virtual Room Room { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartTime { get; set; }

        [Required]
        public string StaffID { get; set; }
        public virtual Staff Staff { get; set; }

        public string StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}
