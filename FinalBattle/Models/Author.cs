using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Enums.CountryEnum Country { get; set; }

        [DataType(DataType.MultilineText)]
        public string Info { get; set; }


        public virtual ICollection<SongAuthor> SongAuthors { get; set; }

    }
}