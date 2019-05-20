using FinalBattle.Data;
using FinalBattle.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBattle.HelpfulClasses
{
    public static class GlobalStatic
    {
        public static string pathImageFolder = "images";
        public static string pathGalleryFolder = "Gallery";

        //public static string GeneratePhotoPath(Photo p)
        //{
        //    //return Path.Combine(pathImageFolder, pathGalleryFolder, p.Name);
        //}
    }
}
