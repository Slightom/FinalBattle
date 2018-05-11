using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class ProgrammeViewModel
    {
        public int ProgrammeViewModelID { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public SearchModel SearchModel { get; set; }

        public ProgrammeViewModel()
        {
            SearchModel = new SearchModel();
            Songs = new List<Song>();
        }
    }

    public class ProgrammeViewModel2
    {
        public int ProgrammeViewModel2ID { get; set; }
        public IEnumerable<Backing> Backings { get; set; }
        public SearchModel SearchModel { get; set; }

        public ProgrammeViewModel2()
        {
            SearchModel = new SearchModel();
            Backings = new List<Backing>();
        }
    }

    public class EventModel
    {
        public int EventModelID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string PlaceName { get; set; }
        public string Info { get; set; }
        public string UserName { get; set; }
        public string EventType { get; set; }
        public string Color { get; set; }
    }
}