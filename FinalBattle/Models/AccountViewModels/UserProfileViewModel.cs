using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBattle.Models.AccountViewModels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Nick")]
        public string Nick { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string PostText { get; set; }
        public string PostStatus { get; set; }
    }
}
