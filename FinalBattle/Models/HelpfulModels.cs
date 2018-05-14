using FinalBattle.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalBattle.Models
{
    public class SearchCategory
    {
        public int SearchCategoryID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public int songAmount { get; set; }
    }

    public class SearchModel
    {
        public int SearchModelID { get; set; }
        public string SearchWord { get; set; }

        public List<SearchCategory> SearchCategories { get; set; }

        public bool SearchMode { get; set; }

        public SearchModel()
        {
            SearchMode = false;
            SearchCategories = new List<SearchCategory>();
        }
    }

    public class CategoryCheckbox
    {
        public Category Category { get; set; }
        public bool isChecked { get; set; }
    }

    public class Helperek
    {
        public string userID { get; set; }
        public string userName { get; set; }

        public string roleName { get; set; }
        public string roleID { get; set; }
    }


    static public class GlobalData
    {
        public static ApplicationDbContext context { get; set; }
        public static string connectionString { get; set; }
    }
}