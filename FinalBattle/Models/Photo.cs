using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }

        public string Path { get; set; }

        public DateTime DateCreated { get; set; }


        public virtual ICollection<PlacePhoto> PlacePhotos { get; set; }
    }
}