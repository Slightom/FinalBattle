using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class Place
    {
        public int PlaceID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        public virtual ICollection<PlacePhoto> PlacePhotos { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}