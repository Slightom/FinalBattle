using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using FinalBattle.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalBattle.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace FinalBattle.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BackingsController : Controller
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

        public BackingsController(ApplicationDbContext context,
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

        // GET: Backings
        public ActionResult Index()
        {
            var backings = db.Backings.Include(b => b.Song);
            return View(backings.OrderBy(x => x.Song.DisplayTitle).ToList());
        }

        // GET: Backings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Backing backing = db.Backings.Find(id);
            if (backing == null)
            {
                return NotFound();
            }
            return View(backing);
        }

        // GET: Backings/Create
        public ActionResult Create()
        {
            ViewBag.SongID = new SelectList(db.Songs, "SongID", "Title", "----");
            return View();
        }

        // POST: Backings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("BackingID", "Name", "SongID", "BackingStatus", "MainBacking", "Path")] Backing backing)
        {
            if (ModelState.IsValid)
            {

                if (HttpContext.Request.Form.Files != null)
                {
                    var fileName = string.Empty;
                    var newFileName = string.Empty;
                    var filePath = string.Empty;
                    string PathDB = string.Empty;

                    var file = HttpContext.Request.Form.Files[0];

                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        // Combines two strings into a path.
                        filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Music", fileName);

                        // if you want to store path of folder in database
                        PathDB = "/Music/" + fileName;

                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        backing.Name = fileName;
                        backing.Path = PathDB;
                        db.Backings.Add(backing);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.SongID = new SelectList(db.Songs, "SongID", "Title", backing.SongID);
            return View(backing);
        }

        // GET: Backings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Backing backing = db.Backings.Find(id);
            if (backing == null)
            {
                return NotFound();
            }
            ViewBag.SongID = new SelectList(db.Songs, "SongID", "Title", backing.SongID);
            return View(backing);
        }

        // POST: Backings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("BackingID", "Name", "SongID", "BackingStatus", "MainBacking", "Path")] Backing backing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(backing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SongID = new SelectList(db.Songs, "SongID", "Title", backing.SongID);
            return View(backing);
        }

        // GET: Backings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Backing backing = db.Backings.Find(id);
            if (backing == null)
            {
                return NotFound();
            }
            return View(backing);
        }

        // POST: Backings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Backing backing = db.Backings.Find(id);

            //usuwanie starego
            string fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "Music", backing.Name);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.Backings.Remove(backing);
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

        public ActionResult UploadBacking(int id)
        {

            Backing backing = db.Backings.Find(id);
            if (backing == null)
            {
                return NotFound();
            }
            return View(backing);
        }


        //////////////////// controler

        [HttpPost]
        public ActionResult UploadBacking(IFormCollection fc)
        {
            if (HttpContext.Request.Form.Files != null)
            {
                Backing backing = db.Backings.Find(Int32.Parse(fc["BackingID"].ToString()));

                //usuwanie starego
                string fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "Music", backing.Name);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }


                var fileName = string.Empty;
                var filePath= string.Empty;
                var newFileName = string.Empty;
                string PathDB = string.Empty;

                var file = HttpContext.Request.Form.Files[0];

                if (file.Length > 0)
                {
                    //Getting FileName
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    ////Getting file Extension
                    //var FileExtension = Path.GetExtension(fileName);

                    //// concating  FileName + FileExtension
                    //newFileName = fileName + FileExtension;

                    // Combines two strings into a path.
                    filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Music", fileName);

                    ViewBag.filePath = filePath;

                    // if you want to store path of folder in database
                    PathDB = "/Music/" + fileName;
                    PathDB = Path.Combine("Music", fileName);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }


                    Backing b = db.Backings.Where(x => x.BackingID == backing.BackingID).FirstOrDefault();
                    b.Path = PathDB;
                    b.Name = fileName;
                    db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
