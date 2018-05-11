using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [Required]
        public string ApplicationUserID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Text { get; set; }

        [Required]
        public Enums.PostStatusEnum Status { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}