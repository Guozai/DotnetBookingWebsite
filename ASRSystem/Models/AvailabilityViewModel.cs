// Follow Matthew Bolger's detailed email instructions

using System;
using System.ComponentModel.DataAnnotations;

namespace Asr.Models
{
    public class AvailabilityViewModel
    {
        [Required]
        [Display(Name = "Select Date")]
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }
    }
}
