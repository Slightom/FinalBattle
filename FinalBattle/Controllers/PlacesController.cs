using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using FinalBattle.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinalBattle.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using FinalBattle.HelpfulClasses;

namespace FinalBattle.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlacesController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationDbContext db2;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        private List<Song> searchedSongs = new List<Song>();
        private List<Backing> searchedBackings = new List<Backing>();
        // private SearchModel searchModel = null;

        public PlacesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IHostingEnvironment hostingEnvironment)
        {
            db = context;
            db2 = context;

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Places
        public ActionResult Index()
        {

            return View(db.Places.ToList());
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }
            return View(place);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PlaceID", "Name", "Address", "Description")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            var photos = db.PlacePhotos.Include(p => p.Photo).Where(x => x.PlaceID == place.PlaceID).Select(y => y.Photo).ToList();
            ViewBag.Photos = photos;
            ViewBag.photosAmount = photos.Count();

            HttpContext.Session.SetString("selectedPlaceID", id.ToString());
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("PlaceID", "Name", "Address", "Description")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();             

                return RedirectToAction("Index");
            }
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            Photo photo = db.Photos.Find(id);

            //usuwanie starego
            string fullPath = Path.Combine(_hostingEnvironment.WebRootPath, photo.Path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.Photos.Remove(photo);
            db.SaveChanges();

            int placeID = Int32.Parse(HttpContext.Session.GetString("selectedPlaceID"));

            return PartialView("PlacePhotosPartial", db.PlacePhotos.Include(p => p.Photo).Where(x => x.PlaceID == placeID).Select(y => y.Photo).ToList());
        }

        [HttpPost]
        public ActionResult AddPlacePhoto(string id2, IFormCollection fc)
        {
            var id = Int32.Parse(HttpContext.Session.GetString("selectedPlaceID"));

            foreach (var file in HttpContext.Request.Form.Files)
            {
                if (file != null && file.Length > 0)
                {
                    // and optionally write the file to disk
                    var fileName = file.FileName;
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images\\Gallery\\" + fileName);

                    ViewBag.filePath = filePath;

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    Photo p = new Photo();
                    p.Path = "/images/Gallery/" + fileName;
                    p.DateCreated = DateTime.Now;
                    db.Photos.Add(p);
                    db.SaveChanges();


                    PlacePhoto pp = new PlacePhoto();
                    pp.PlaceID = (int)id;
                    pp.PhotoID = db.Photos.Where(zp => zp.Path == "/images/Gallery/" + fileName).Select(x => x.PhotoID).FirstOrDefault();
                    db.PlacePhotos.Add(pp);
                    db.SaveChanges();


                }
            }

            return PartialView("PlacePhotosPartial", db.PlacePhotos.Include(p => p.Photo).Where(x => x.PlaceID == id).Select(y => y.Photo).ToList());
        }
    }
}
