using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class Event
    {
        public int EventID { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }

        public int PlaceID { get; set; }

        [DataType(DataType.MultilineText)]
        public string Info { get; set; }
        public Enums.EventType EventType { get; set; }
        public string UserID { get; set; }


        public virtual Place Place { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}