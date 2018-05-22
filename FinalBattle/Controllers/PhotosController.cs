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
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace FinalBattle.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhotosController : Controller
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

        public PhotosController(ApplicationDbContext context,
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

        // GET: Photos
        public ActionResult Index()
        {
            var placePhotos = db.PlacePhotos.Select(x => x.PhotoID).ToList();
            List<Photo> photos = new List<Photo>();

            foreach(var p in db.Photos)
            {
                if(!placePhotos.Contains(p.PhotoID))
                {
                    photos.Add(p);
                }
            }
            return View(photos);
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PhotoID", "Path", "DateCreated")] Photo photo)
        {
            if (ModelState.IsValid)
            {

                if (HttpContext.Request.Form.Files != null)
                {
                    var fileName = string.Empty;
                    var filePath = string.Empty;

                    var file = HttpContext.Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        // Combines two strings into a path.
                        filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images\\Gallery\\" + fileName);

                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        photo.Path = "images/Photos/Gallery/" + fileName;
                        photo.DateCreated = DateTime.Now;
                        db.Photos.Add(photo);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("PhotoID", "Path", "DateCreated")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
    }
}
