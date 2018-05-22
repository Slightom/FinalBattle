using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class PlacePhoto
    {
        public int PlaceID { get; set; }

        public int PhotoID { get; set; }


        public virtual Place Place { get; set; }
        public virtual Photo Photo { get; set; }
    }
}