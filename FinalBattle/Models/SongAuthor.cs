using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class SongAuthor
    {
        public int SongID { get; set; }

        public int AuthorID { get; set; }

        public bool MainSongAuthor { get; set; }

        public virtual Song Song { get; set; }
        public virtual Author Author { get; set; }
    }
}