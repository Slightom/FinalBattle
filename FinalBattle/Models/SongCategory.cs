using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class SongCategory
    {
        public int SongID { get; set; }

        public int CategoryID { get; set; }


        public virtual Category Category { get; set; }
        public virtual Song Song { get; set; }
    }
}