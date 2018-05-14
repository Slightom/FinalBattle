using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalBattle.Models;
using FinalBattle.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using FinalBattle.HelpfulClasses;

namespace FinalBattle.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationDbContext db2;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        private List<Song> searchedSongs = new List<Song>();
        private List<Backing> searchedBackings = new List<Backing>();
        // private SearchModel searchModel = null;

        public HomeController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            db = context;
            db2 = context;
            GlobalData.context = context;

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public ActionResult Index()
        {
            var posts = db.Posts.Where(x => x.Status == Enums.PostStatusEnum.Approved).Include(p => p.ApplicationUser).ToList();
            posts = posts.OrderByDescending(x => x.Date).ToList();
            return View(posts);
        }


        public ActionResult People()
        {
            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            var placePhotos = db.PlacePhotos.Select(y => y.PhotoID).Distinct();
            var photos = db.Photos.Where(
                    x => !placePhotos.Contains(x.PhotoID)
                ).ToList();
            return View(photos);
        }

        public ActionResult Contact()
        {
            return View();
        }

        #region PROGRAMME REGION

        #region programme Standard Edition

        public ActionResult Programme(SearchModel sm)
        {

            ProgrammeViewModel programmeViewModel;
            List<Song> songs = new List<Song>();
            SearchModel searchModel;

            searchModel = new SearchModel();
            searchModel.SearchCategories = new List<SearchCategory>();
            SearchCategory sc;
            foreach (var c in db.Categories)
            {
                sc = new SearchCategory();
                sc.Name = c.Name;
                sc.Checked = true;

                sc.songAmount = db2.SongCategories.Where(x => x.CategoryID == c.CategoryID).Count();

                searchModel.SearchCategories.Add(sc);
            }
            songs = db.Songs.OrderBy(x => x.DisplayTitle).ToList();


            programmeViewModel = new ProgrammeViewModel();
            programmeViewModel.SearchModel = searchModel;
            programmeViewModel.Songs = songs;

            ViewBag.SongList = songs;

            return View(programmeViewModel);
        }
        
        [HttpPost]
        public ActionResult ReplacePartialView()
        {
            var songs = HttpContext.Session.GetObject<List<Song>>("searchedSongs");

            return PartialView("SearchPartial", HttpContext.Session.GetObject<List<Song>>("searchedSongs") as List<Song>);
        }

        [HttpPost]
        public JsonResult SearchSong(string SearchWord, string SearchList)
        {
            //if (SearchWord.Contains("Backspace"))
            //{
            //    SearchWord = SearchWord.Replace("Backspace", "");
            //    if (SearchWord.Length > 0)
            //    {
            //        SearchWord = SearchWord.Remove(SearchWord.Length - 1, 1);
            //    }
            //}

            //if(SearchWord.Contains("Unidentified"))
            //{
            //    SearchWord = SearchWord.Replace("Unidentified", "");
            //}

            if (!String.IsNullOrEmpty(SearchList))
            {
                List<int> selectedCategoriesIDs = retrieveCategoriesIDs(SearchList);

                var songs = db.Songs.Include(x => x.SongCategories);
                foreach (var s in songs)
                {
                    var categoriesIDs = s.SongCategories.Select(x => x.CategoryID);
                    foreach (var sc in categoriesIDs)
                    {
                        foreach (var scc in selectedCategoriesIDs)
                        {
                            if (scc == sc)
                            {
                                s.SongCategories = null;
                                searchedSongs.Add(s);
                                goto SongFinded;
                            }
                        }
                    }

                    SongFinded:;
                }
            }

            //searchedSongs = db.Songs.ToList();

            if (!String.IsNullOrEmpty(SearchWord))
            {
                searchedSongs = searchedSongs.Where(x => x.DisplayTitle.ToLower().Contains(SearchWord.ToLower())).ToList();
            }


           HttpContext.Session.SetObject("searchedSongs", searchedSongs);

            return Json("ok");
        }

        #endregion

        [Authorize(Roles = "BandMember")]
        public ActionResult Programme2(SearchModel sm)
        {

            ProgrammeViewModel2 programmeViewModel2;
            List<Backing> backings = new List<Backing>();
            SearchModel searchModel;

            searchModel = new SearchModel();
            searchModel.SearchCategories = new List<SearchCategory>();
            SearchCategory sc;
            foreach (var c in db.Categories)
            {
                sc = new SearchCategory();
                sc.Name = c.Name;
                sc.Checked = true;

                sc.songAmount = db2.SongCategories.Where(x => x.CategoryID == c.CategoryID).Count();

                searchModel.SearchCategories.Add(sc);
            }
            backings = db.Backings.OrderBy(x => x.Name).ToList();


            programmeViewModel2 = new ProgrammeViewModel2();
            programmeViewModel2.SearchModel = searchModel;
            programmeViewModel2.Backings = backings;

            ViewBag.BackingList = backings;

            return View(programmeViewModel2);
        }


        public ActionResult ReplacePartialView2()
        {
            return PartialView("SearchPartial2", HttpContext.Session.GetObject<List<Backing>>("searchedBackings") as List<Backing>);
        }

        [Authorize(Roles = "BandMember")]
        [HttpPost]
        public JsonResult SearchBacking2(string SearchWord, string SearchList)
        {
            //if (SearchWord.Contains("Backspace"))
            //{
            //    SearchWord = SearchWord.Replace("Backspace", "");
            //    if (SearchWord.Length > 0)
            //    {
            //        SearchWord = SearchWord.Remove(SearchWord.Length - 1, 1);
            //    }
            //}

            if (!String.IsNullOrEmpty(SearchList))
            {
                List<int> selectedCategoriesIDs = retrieveCategoriesIDs(SearchList);
                List<Backing> backs;

                var songs = db.Songs.Include(x => x.SongCategories).Include(y => y.Backings);
                foreach (var s in songs)
                {
                    var categoriesIDs = s.SongCategories.Select(x => x.CategoryID);
                    foreach (var sc in categoriesIDs)
                    {
                        foreach (var scc in selectedCategoriesIDs)
                        {
                            if (scc == sc)
                            {
                                foreach (var b in s.Backings)
                                {
                                    searchedBackings.Add(b);
                                }
                                goto SongFinded;
                            }
                        }
                    }

                    SongFinded:;
                }
            }

            //searchedSongs = db.Songs.ToList();

            if (!String.IsNullOrEmpty(SearchWord))
            {
                searchedBackings = searchedBackings.Where(x => x.Name.ToLower().Contains(SearchWord.ToLower())).ToList();
            }


            HttpContext.Session.SetObject("searchedBackings", searchedBackings);

            return Json("ok");
        }

        #region programme for Band Members

        #endregion

        private List<int> retrieveCategoriesIDs(string searchList)
        {
            List<int> result = new List<int>();
            searchList = searchList.Remove(searchList.Length - 1, 1);
            String[] tab = searchList.Split('%');

            var categories = db.Categories.Select(x => new { x.CategoryID, x.Name }).ToList();
            foreach (var c in tab)
            {
                result.Add(categories.Where(y => y.Name == c).Select(x => x.CategoryID).FirstOrDefault());
            }

            return result;
        }
        #endregion

        public async Task<string> PostPopupAsync(string postText)
        {
            Post p = new Post();
            p.Date = DateTime.Now;
            p.ApplicationUserID = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            p.Status = Enums.PostStatusEnum.New;
            p.Text = postText;

            db.Posts.Add(p);
            db.SaveChanges();

            return "Dziękujęmy za napisanie opinii na nasz temat ;) Pojawi się ona na stronie po akceptacji administratora.";
        }

        [Authorize(Roles = "BandMember")]
        public JsonResult GetEvents()
        {
            List<EventModel> list = new List<EventModel>();
            EventModel em;

            foreach (var e in db.Events.Include(x => x.User))
            {
                em = new EventModel();
                em.EventModelID = e.EventID;
                em.Title = e.Title;
                em.Date = e.Date;
                em.DateEnd = e.DateEnd;
                em.Info = e.Info;
                em.UserName = e.User.UserName;
                em.EventType = e.EventType.ToString();
                em.PlaceName = db2.Places.Where(x => x.PlaceID == e.PlaceID).Select(y => y.Name + " (" + y.Address + ")").FirstOrDefault();
                if (e.EventType == Enums.EventType.granie)
                {
                    em.Color = "green";
                }
                else
                {
                    em.Color = "#ff0000e6";
                }

                list.Add(em);

            }

            return Json(list);
        }

        [Authorize(Roles = "BandMember")]
        [HttpPost]
        public JsonResult SaveEvent(EventModel em)
        {
            var status = false;

            if (em.EventModelID > 0)
            {
                //update event
                Event e = db.Events.Find(em.EventModelID);
                if (e != null)
                {
                    e.Title = em.Title;
                    e.Date = em.Date;
                    e.DateEnd = em.DateEnd;
                    e.Info = em.Info;
                    e.UserID = e.UserID;
                    if ((Enums.EventType)Enum.Parse(typeof(Enums.EventType), em.EventType) == Enums.EventType.granie)
                    {
                        e.PlaceID = db.Places.Where(y => em.PlaceName.Contains(y.Name)).Select(x => x.PlaceID).FirstOrDefault();
                    }
                    else
                    {
                        e.PlaceID = db.Places.Where(x => x.Name == "EmptyPlace").Select(y => y.PlaceID).FirstOrDefault();
                    }
                    e.EventType = (Enums.EventType)Enum.Parse(typeof(Enums.EventType), em.EventType);
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                //save new
                Event e = new Event();
                e.Title = em.Title;
                e.Date = em.Date;
                e.DateEnd = em.DateEnd;
                if ((Enums.EventType)Enum.Parse(typeof(Enums.EventType), em.EventType) == Enums.EventType.granie)
                {
                    e.PlaceID = db.Places.Where(y => em.PlaceName.Contains(y.Name)).Select(x => x.PlaceID).FirstOrDefault();
                }
                else
                {
                    e.PlaceID = db.Places.Where(x => x.Name == "EmptyPlace").Select(y => y.PlaceID).FirstOrDefault();
                }
                e.Info = em.Info;
                e.UserID = HttpContext.User.Identity.Name;
                e.EventType = (Enums.EventType)Enum.Parse(typeof(Enums.EventType), em.EventType);
                db.Events.Add(e);
                db.SaveChanges();
            }

            status = true;

            return Json(status);// { Data = new { status = status } };
        }

        [Authorize(Roles = "BandMember")]
        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;

            var e = db.Events.Find(eventID);
            if (e != null)
            {
                db.Events.Remove(e);
                db.SaveChanges();
                status = true;
            }


            return Json(status);
        }

        [Authorize(Roles = "BandMember")]
        public ActionResult Calender()
        {
            List<string> listString = Enum.GetNames(typeof(Enums.EventType)).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItem> list2 = new List<SelectListItem>();
            int i = 0;
            foreach (var s in listString)
            {
                list.Add(new SelectListItem() { Text = s, Value = s });
            }

            foreach (var p in db.Places)
            {
                if (p.Name != "EmptyPlace")
                {
                    list2.Add(new SelectListItem() { Text = p.Name + " (" + p.Address + ")", Value = p.Name + " (" + p.Address + ")" });
                }
            }


            ViewBag.EventType = new SelectList(list, "Value", "Text", listString[0]);
            ViewBag.Place = new SelectList(list2, "Value", "Text", db.Places.Select(p => p.Name + " (" + p.Address + ")").FirstOrDefault());

            return View();
        }

        [Authorize(Roles = "BandMember")]
        public JsonResult GetPlacePhotos(string placeNameAddress)
        {
            var placeID = db.Places.Where(x => placeNameAddress.Contains(x.Address)).Select(y => y.PlaceID).FirstOrDefault();
            var photoplaces = db.PlacePhotos.Include(p => p.Photo).Where(x => x.PlaceID == placeID).Select(p => new { p.Photo.PhotoID, p.Photo.Path }).ToList();

            return Json(photoplaces);
        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UsersAsync()
        {
            List<Helperek> userRoles = new List<Helperek>();
            string roleName;
            Helperek h;
            ApplicationUser logged = (await _userManager.GetUserAsync(HttpContext.User));
            var roles = _roleManager.Roles.ToList();
            var users = db.Users.Include(x => x.Roles).ToList();


            foreach (var u in users)
            {
                foreach (var r in u.Roles)
                {
                    roleName = roles.Where(x => x.Id == r.RoleId).FirstOrDefault().Name;
                    h = new Helperek();
                    h.userID = u.Id;
                    h.userName = u.UserName;
                    h.roleID = r.RoleId;
                    h.roleName = roleName;

                    userRoles.Add(h);
                }
            }

            return View(userRoles.OrderBy(x => x.roleName));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateUserRole()
        {
            List<SelectListItem> userList = new List<SelectListItem>();
            List<SelectListItem> roleList = new List<SelectListItem>();
            int i = 0;
            foreach (var u in db.Users)
            {
                userList.Add(new SelectListItem() { Text = u.UserName, Value = u.Id });
            }

            foreach (var r in _roleManager.Roles.ToList())
            {
                roleList.Add(new SelectListItem() { Text = r.Name, Value = r.Name });
            }

            ViewBag.UserList = userList;
            ViewBag.RoleList = roleList;

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateUserRole(FormCollection fc)
        {
            string userID = fc["UserList"].ToString();
            string roleName = fc["RoleList"].ToString();

           _userManager.AddToRoleAsync(db.Users.Find(userID), roleName);

            return RedirectToAction("Users");
        }

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public ActionResult DeleteUserRole(string userID, string roleID)
        //{

        //    (new IdentityManager()).LocalUserManager.RemoveFromRole(userID, roleID);

        //    return RedirectToAction("Users");
        //}

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUserRole(string userID, string roleName)
        {
            _userManager.RemoveFromRoleAsync(db.Users.Find(userID), roleName);

            return RedirectToAction("Users");
        }
    }
}
