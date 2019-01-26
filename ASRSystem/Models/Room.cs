// Model copied from Matthew Bolger's model of Week7 example code

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asr.Models
{
    public class Room
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RoomID { get; set; }

        public virtual ICollection<Slot> Slots { get; set; }
    }
}
