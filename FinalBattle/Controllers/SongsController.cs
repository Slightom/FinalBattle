using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalBattle.Data;
using FinalBattle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FinalBattle.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SongsController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationDbContext db2;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public SongsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            db = context;
            db2 = context;

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Songs
        public ActionResult Index()
        {
            return View(db.Songs.OrderBy(x => x.DisplayTitle).ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            ViewBag.Authors = new SelectList(db.Authors.ToList(), "AuthorID", "Name", "----");
            ViewBag.Authors2 = new SelectList(db.Authors.ToList(), "AuthorID", "Name", "----");
            ViewBag.Authors3 = new SelectList(db.Authors.ToList(), "AuthorID", "Name", "----");
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.CategoriesAmount = db.Categories.Count();
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("SongID", "Title", "DisplayTitle", "WithBacking", "Info", "TextLanguage", "AuthorInTitle")] Song song, IFormCollection fc)
        {
            if (song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            if (ModelState.IsValid)
            {
                db.Songs.Add(song);
                db.SaveChanges();

                int songID = db.Songs.Where(x => x.Title == song.Title).Select(y => y.SongID).FirstOrDefault();

                ////handle with authors
                #region songAuthors
                StringValues a, a2, a3;
                

                SongAuthor sa;
                if (fc.TryGetValue("Authors", out a) && a[0] != "-1")
                {
                    sa = new SongAuthor();
                    sa.SongID = songID;
                    sa.AuthorID = Int32.Parse(a);
                    db.SongAuthors.Add(sa);
                    db.SaveChanges();
                }

                if (fc.TryGetValue("Authors2", out a2) && a2[0] != "-1")
                {
                    sa = new SongAuthor();
                    sa.SongID = songID;
                    sa.AuthorID = Int32.Parse(a2);
                    db.SongAuthors.Add(sa);
                    db.SaveChanges();
                }

                if (fc.TryGetValue("Authors3", out a3) && a3[0] != "-1")
                {
                    sa = new SongAuthor();
                    sa.SongID = songID;
                    sa.AuthorID = Int32.Parse(a3);
                    db.SongAuthors.Add(sa);
                    db.SaveChanges();
                }
                #endregion

                // handle with categories
                foreach (var f in fc)
                {
                    string sKey = f.Key;
                    if (sKey.Contains("Category-"))
                    {
                        sKey = sKey.Replace("Category-", "");
                        int categoryID = Int32.Parse(sKey);

                        SongCategory sc = new SongCategory();
                        sc.SongID = songID;
                        sc.CategoryID = categoryID;
                        db.SongCategories.Add(sc);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }

            var songAuthorList = db.SongAuthors.Where(x => x.SongID == song.SongID).ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItem> list2 = new List<SelectListItem>();
            List<SelectListItem> list3 = new List<SelectListItem>();

            list.Add(new SelectListItem() { Text = "----", Value = "-1" });
            list2.Add(new SelectListItem() { Text = "----", Value = "-1" });
            list3.Add(new SelectListItem() { Text = "----", Value = "-1" });
            foreach (var l in db.Authors)
            {
                list.Add(new SelectListItem() { Text = l.Name, Value = l.AuthorID.ToString() });
                list2.Add(new SelectListItem() { Text = l.Name, Value = l.AuthorID.ToString() });
                list3.Add(new SelectListItem() { Text = l.Name, Value = l.AuthorID.ToString() });
            }

            ViewBag.Authors = new SelectList(list, "Value", "Text", songAuthorList.Count() > 0 ? songAuthorList[0].Author.AuthorID.ToString() : "----");
            ViewBag.Authors2 = new SelectList(list2, "Value", "Text", songAuthorList.Count() > 1 ? songAuthorList[1].Author.AuthorID.ToString() : "----");
            ViewBag.Authors3 = new SelectList(list3, "Value", "Text", songAuthorList.Count() > 2 ? songAuthorList[2].Author.AuthorID.ToString() : "----");
            ViewBag.AuthorsAmount = songAuthorList.Count();

            var Categories = new List<CategoryCheckbox>();
            CategoryCheckbox cc;
            foreach (var c in db.Categories)
            {
                cc = new CategoryCheckbox();
                cc.Category = c;
                cc.isChecked = db2.SongCategories.Where(x => x.SongID == song.SongID && x.CategoryID == c.CategoryID).Count() == 1 ? true : false;
                Categories.Add(cc);
            }

            ViewBag.Categories = Categories;
            ViewBag.CategoriesAmount = db.Categories.Count();
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("SongID", "Title", "DisplayTitle", "WithBacking", "Info", "TextLanguage", "AuthorInTitle")] Song song, IFormCollection fc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();

                ////handle with authors
                #region songAuthors
                StringValues a, a2, a3;

                foreach (var s in db.SongAuthors.Where(x => x.SongID == song.SongID).ToList())
                {
                    db.SongAuthors.Remove(s);
                    db.SaveChanges();
                }

                SongAuthor sa;
                if (fc.TryGetValue("Authors", out a) && a[0] != "-1")
                {
                    sa = new SongAuthor();
                    sa.SongID = song.SongID;
                    sa.AuthorID = Int32.Parse(a);
                    db.SongAuthors.Add(sa);
                    db.SaveChanges();
                }

                if (fc.TryGetValue("Authors2", out a2) && a2[0] != "-1")
                {
                    sa = new SongAuthor();
                    sa.SongID = song.SongID;
                    sa.AuthorID = Int32.Parse(a2);
                    db.SongAuthors.Add(sa);
                    db.SaveChanges();
                }

                if (fc.TryGetValue("Authors3", out a3) && a3[0] != "-1")
                {
                    sa = new SongAuthor();
                    sa.SongID = song.SongID;
                    sa.AuthorID = Int32.Parse(a3);
                    db.SongAuthors.Add(sa);
                    db.SaveChanges();
                }
                #endregion

                // handle with categories
                foreach (var c in db.SongCategories.Where(x => x.SongID == song.SongID).ToList())
                {
                    db.SongCategories.Remove(c);
                    db.SaveChanges();
                }

                foreach (var f in fc)
                {
                    string s = f.Key;
                    if (s.Contains("Category-"))
                    {
                        s = s.Replace("Category-", "");
                        int categoryID = Int32.Parse(s);

                        SongCategory sc = new SongCategory();
                        sc.SongID = song.SongID;
                        sc.CategoryID = categoryID;
                        db.SongCategories.Add(sc);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();

            //foreach (var b in db.Backings.Where(x => x.SongID == id).ToList())
            //{
            //    db.Backings.Remove(b);
            //    db.SaveChanges();
            //}

            //foreach (var b in db.SongCategories.Where(x => x.SongID == id).ToList())
            //{
            //    db.SongCategories.Remove(b);
            //    db.SaveChanges();
            //}

            //foreach (var b in db.SongAuthors.Where(x => x.SongID == id).ToList())
            //{
            //    db.SongAuthors.Remove(b);
            //    db.SaveChanges();
            //}

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
