using FinalBattle.Data;
using FinalBattle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinalBattle.Migrations
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .

           

            DeleteAllRecords(context);

            // Look for any students.
            if (context.Songs.Any())
            {
                return;   // DB has been seeded
            }

            HandleRolesAsync(roleManager).Wait();
            


            #region users
            ApplicationUser u =  new ApplicationUser { UserName = "Tomek", Email ="tomasz.suchwalko@gmail.com" };
            userManager.CreateAsync(u, "Slightom20p+").Wait();
            u = context.Users.Where(x => x.UserName == u.UserName).FirstOrDefault();
            userManager.AddToRoleAsync(u, "Admin").Wait();
            userManager.AddToRoleAsync(u, "BandMember").Wait();

            u = new ApplicationUser { UserName = "Stefan", Email = "zwyklyMail@gmail.com" };
            userManager.CreateAsync(u, "Stefan20p+").Wait();
            u = context.Users.Where(x => x.UserName == u.UserName).FirstOrDefault();
            userManager.AddToRoleAsync(u, "BandMember").Wait();

            u = new ApplicationUser { UserName = "szalonaOla20", Email = "szalonaOla@gmail.com" };
            userManager.CreateAsync(u, "SzalonaOla20p+").Wait();
            u = context.Users.Where(x => x.UserName == u.UserName).FirstOrDefault();
            userManager.AddToRoleAsync(u, "User").Wait();

            u = new ApplicationUser { UserName = "witek15", Email = "witek15@gmail.com" };
            userManager.CreateAsync(u, "Witek15+").Wait();
            u = context.Users.Where(x => x.UserName == u.UserName).FirstOrDefault();
            userManager.AddToRoleAsync(u, "User").Wait();
            #endregion


            #region posts
            Post p = new Post();
            string ids;
            ids = context.Users.Where(n => n.UserName == "witek15").Select(n => n.Id).FirstOrDefault();
            p.ApplicationUserID = ids;
            p.Text = "Świetny zespół!! Bawiliśmy się cudownie do białego rana, polecam.";
            p.Status = Enums.PostStatusEnum.Approved;
            p.Date = new DateTime(2017, 10, 08, 10, 10, 10);
            context.Posts.Add(p);
            context.SaveChanges();

            p = new Post();
            ids = context.Users.Where(n => n.UserName == "szalonaOla20").Select(n => n.Id).FirstOrDefault();
            p.ApplicationUserID = ids;
            p.Text = "Gorąco polecam, super zespół. Pięknie zagrane, extra zaśpiewane, 10/10.";
            p.Status = Enums.PostStatusEnum.Approved;
            p.Date = new DateTime(2017, 06, 28, 11, 11, 11);
            context.Posts.Add(p);
            context.SaveChanges();

            p = new Post();
            ids = context.Users.Where(n => n.UserName == "szalonaOla20").Select(n => n.Id).FirstOrDefault();
            p.ApplicationUserID = ids;
            p.Text = "fatalny zespół ja pierniczę";
            p.Status = Enums.PostStatusEnum.Rejected;
            p.Date = new DateTime(2017, 07, 24, 12, 12, 12);
            context.Posts.Add(p);
            context.SaveChanges();
            #endregion


            #region categories
            Category c = new Category();
            c.Name = "Polskie";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Zagraniczne";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Disco polo";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Biesiadne i przyśpiewki";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Rock";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Pop";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Wolne";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Alternatywa";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Propozycje na I taniec";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Propozycje na podziękowania rodzicom";
            context.Categories.Add(c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Oprawa ślubów";
            context.Categories.Add(c);
            context.SaveChanges();
            #endregion


            #region authors
            Author a = new Author();
            a.Name = "Akcent";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Exaited";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Andrzej Piaseczny";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Andrzej Rybiński";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Anna Jantar";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Antek Mójkowski";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Arctic Monkeys";
            a.Country = Enums.CountryEnum.GB;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Bad Boys Blue";
            a.Country = Enums.CountryEnum.Niemcy;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Big Cyc";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Basia Stępniak-Wilk";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Grzegorz Turnau";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Boys";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Bracia";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Chubby Checker";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Cliver";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Biesiadne";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Czerwone Gitary";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "David Gray";
            a.Country = Enums.CountryEnum.GB;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Defis";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Marcin Miller";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Edyta Górniak";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Mieczysław Szcześniak";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Dżem";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Earth, Wind & Fire";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Elvis Presley";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Enej";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Fancy";
            a.Country = Enums.CountryEnum.Niemcy;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Frank Sinatra";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "George Baker";
            a.Country = Enums.CountryEnum.Holandia;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Golec Uorkiestra";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Green Day";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Grzegorz Tomczak";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Gusttavo";
            a.Country = Enums.CountryEnum.Brazylia;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Ich troje";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Janusz Laskowski";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Jerzy Połomski";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Jorrgus";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Kombi";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Krzysztof Krawczyk";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Lady Pank";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Lionel Richie";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Maciej Kossowski";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Mamzel";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Marek Grechuta";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Marek Tranda";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Masters";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Michel Telo";
            a.Country = Enums.CountryEnum.Brazylia;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Mig";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Modern Talking";
            a.Country = Enums.CountryEnum.Niemcy;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Nazir";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Paweł Kukiz";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Piersi";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Piękni i Młodzi";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Pin";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Piotr Szczepanik";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Poparzeni Kawą Trzy";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Rezonans";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Rihanna";
            a.Country = Enums.CountryEnum.Barbados;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Mikky Ekko";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Seweryn Krajewski";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Shakin' Stevens";
            a.Country = Enums.CountryEnum.Walia;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Shantel";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Skaldowie";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Sławomir";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Tom Jones";
            a.Country = Enums.CountryEnum.GB;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Village People";
            a.Country = Enums.CountryEnum.USA;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Weekend";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            a = new Author();
            a.Name = "Wilki";
            a.Country = Enums.CountryEnum.Polska;
            context.Authors.Add(a);
            context.SaveChanges();

            #endregion


            #region songs, songCategories, songAuthors

            int catIdPol = context.Categories.Where(x => x.Name == "Polskie").Select(y => y.CategoryID).First();
            int catIdZag = context.Categories.Where(x => x.Name == "Zagraniczne").Select(y => y.CategoryID).First();
            int catIdDis = context.Categories.Where(x => x.Name == "Disco polo").Select(y => y.CategoryID).First();
            int catIdBip = context.Categories.Where(x => x.Name == "Biesiadne i przyśpiewki").Select(y => y.CategoryID).First();
            int catIdRck = context.Categories.Where(x => x.Name == "Rock").Select(y => y.CategoryID).First();
            int catIdPn1 = context.Categories.Where(x => x.Name == "Propozycje na I taniec").Select(y => y.CategoryID).First();
            int catIdPnp = context.Categories.Where(x => x.Name == "Propozycje na podziękowania rodzicom").Select(y => y.CategoryID).First();
            int catIdOpś = context.Categories.Where(x => x.Name == "Oprawa ślubów").Select(y => y.CategoryID).First();
            int catIdWol = context.Categories.Where(x => x.Name == "Wolne").Select(y => y.CategoryID).First();
            int catIdPop = context.Categories.Where(x => x.Name == "Pop").Select(y => y.CategoryID).First();
            int catIdAlt = context.Categories.Where(x => x.Name == "Alternatywa").Select(y => y.CategoryID).First();


            Song s = new Song();
            s.Title = "Biorę urlop od Ciebie";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Akcent - Biorę urlop od Ciebie";
            context.Songs.Add(s);
            context.SaveChanges();
            int songID = context.Songs.Where(x => x.Title == "Biorę urlop od Ciebie").Select(y => y.SongID).First();

            SongCategory sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            SongAuthor sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Czekam na Ciebie";
            s.WithBacking = true;
            s.DisplayTitle = "Akcent - Czekam na Ciebie";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            s.AuthorInTitle = true;
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Czekam na Ciebie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Kochana wierzę w miłość";
            s.WithBacking = true;
            s.DisplayTitle = "Akcent - Kochana wierzę w miłość";
            s.TextLanguage = Enums.LanguageEnum.polski;
            s.AuthorInTitle = true;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Kochana wierzę w miłość").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Królowa nocy";
            s.DisplayTitle = "Akcent - Królowa nocy";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Królowa nocy").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Prawdziwa miłość to Ty";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Akcent - Prawdziwa miłość to Ty";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Prawdziwa miłość to Ty").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Przekorny los";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Akcent - Przekorny los";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Przekorny los").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Przez Twe oczy zielone";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Akcent - Przez Twe oczy zielone";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Przez Twe oczy zielone").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Sonet dla miłości";
            s.WithBacking = true;
            s.DisplayTitle = "Akcent - Sonet dla miłości";
            s.TextLanguage = Enums.LanguageEnum.polski;
            s.AuthorInTitle = true;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Sonet dla miłości").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Taką Cię wyśniłem";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Akcent - Taką Cię wyśniłem";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Taką Cię wyśniłem").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "W sercu mi graj";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Akcent & Exaited - W sercu mi graj";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "W sercu mi graj").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = false;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Exaited").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Życie to są chwile";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Akcent - Życie to są chwile";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Życie to są chwile").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Akcent").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Chodź, przytul, przebacz";
            s.WithBacking = true;
            s.DisplayTitle = "Andrzej Piaseczny - Chodź, przytul, przebacz";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Chodź, przytul, przebacz").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Andrzej Piaseczny").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Nie liczę godzin i lat";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Andrzej Rybiński - Nie liczę godzin i lat";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Nie liczę godzin i lat").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Andrzej Rybiński").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Moje jedyne marzenie";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Anna Jantar - Moje jedyne marzenie";
            s.TextLanguage = Enums.LanguageEnum.polski;
            s.Info = "Przetańczyć z Tobą chcę całą noc";
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Moje jedyne marzenie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Anna Jantar").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Bogini niepojęta";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Antek Mójkowski - Bogini niepojęta";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Bogini niepojęta").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Antek Mójkowski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "No 1. Parthy anthem";
            s.WithBacking = true;
            s.DisplayTitle = "Arctic Monkeys - No 1. Parthy antehm ^1";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "No 1. Parthy anthem").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Arctic Monkeys").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "You're a woman, I'm a man";
            s.WithBacking = true;
            s.DisplayTitle = "Bad Boys Blue - You're a woman, I'm a man";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "You're a woman, I'm a man").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Bad Boys Blue").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Cyganeczka Zosia";
            s.WithBacking = true;
            s.DisplayTitle = "Cyganeczka Zosia";
            s.AuthorInTitle = false;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Cyganeczka Zosia").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdBip;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Biesiadne").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Rudy się żeni";
            s.WithBacking = true;
            s.DisplayTitle = "Big Cyc - Rudy się żeni";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Rudy się żeni").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Big Cyc").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Bombonierka";
            s.WithBacking = true;
            s.DisplayTitle = "Bombonierka";
            s.AuthorInTitle = false;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Bombonierka").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdAlt;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Basia Stępniak-Wilk").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = false;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Grzegorz Turnau").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Moja Kochana";
            s.WithBacking = true;
            s.DisplayTitle = "Boys - Moja Kochana";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Moja Kochana").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Boys").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Za szkłem";
            s.WithBacking = true;
            s.DisplayTitle = "Bracia - Za szkłem";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Za szkłem").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Bracia").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Let's twist again";
            s.WithBacking = true;
            s.DisplayTitle = "Chubby Checker - Let's twist again";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Let's twist again").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Chubby Checker").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Moje ciało oszalało";
            s.WithBacking = true;
            s.DisplayTitle = "Cliver - Moje ciało oszalało";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Moje ciało oszalało").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Cliver").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Wezmę Cię ze sobą";
            s.WithBacking = true;
            s.DisplayTitle = "Czerwone Gitary - Wezmę Cię ze sobą";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Wezmę Cię ze sobą").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Czerwone Gitary").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "This Year's Love";
            s.WithBacking = true;
            s.DisplayTitle = "David Gray - This Year's Love";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "This Year's Love").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "David Gray").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Zakochane oczy";
            s.WithBacking = true;
            s.DisplayTitle = "Defis & Marcin Miller - Zakochane oczy";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Zakochane oczy").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Defis").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = false;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Marcin Miller").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Dumka na dwa serca";
            s.WithBacking = true;
            s.DisplayTitle = "Dumka na dwa serca";
            s.AuthorInTitle = false;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Dumka na dwa serca").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Edyta Górniak").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = false;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mieczysław Szcześniak").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Do kołyski";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Dżem - Do kołyski";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Do kołyski").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Dżem").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "September";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Earth, Wind & Fire - September";
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "September").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Earth, Wind & Fire").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Always on my mind";
            s.WithBacking = true;
            s.DisplayTitle = "Elvis Presley - Always on my mind";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Always on my mind").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPn1;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Elvis Presley").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Burning Love";
            s.WithBacking = true;
            s.DisplayTitle = "Elvis Presley - Burning Love";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Burning Love").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Elvis Presley").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Can't help falling in love";
            s.WithBacking = true;
            s.DisplayTitle = "Elvis Presley - Can't help falling in love";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Can't help falling in love").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPn1;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Elvis Presley").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Kamień z napisem Love";
            s.WithBacking = true;
            s.DisplayTitle = "Enej - Kamień z napisem Love";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Kamień z napisem Love").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Enej").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Skrzydlate ręce";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Enej - Skrzydlate ręce";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Skrzydlate ręce").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Enej").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Flames of love";
            s.WithBacking = true;
            s.DisplayTitle = "Fancy - Flames of love";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Flames of love").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Fancy").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();


            s = new Song();
            s.Title = "My way";
            s.DisplayTitle = "Frank Sinatra - My Way";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "My way").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Frank Sinatra").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Uno Paloma Blanca";
            s.WithBacking = true;
            s.DisplayTitle = "George Baker - Uno Paloma Blanca";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Uno Paloma Blanca").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "George Baker").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Pędzą konie po betonie";
            s.WithBacking = true;
            s.DisplayTitle = "Golec Uorkiestra - Pędzą konie po betonie";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Pędzą konie po betonie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Golec Uorkiestra").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Pieniądze to nie wszystko";
            s.WithBacking = true;
            s.DisplayTitle = "Golec Uorkiestra - Pieniądze to nie wszystko";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Pieniądze to nie wszystko").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Golec Uorkiestra").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Słodycze";
            s.WithBacking = true;
            s.DisplayTitle = "Golec Uorkiestra - Słodycze";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Słodycze").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Golec Uorkiestra").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Ściernisko";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Golec Uorkiestra - Ściernisko";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Ściernisko").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Golec Uorkiestra").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Życie jest muzyką";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Golec Uorkiestra - Życie jest muzyką";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Życie jest muzyką").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Golec Uorkiestra").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();


            s = new Song();
            s.Title = "21 guns";
            s.WithBacking = true;
            s.DisplayTitle = "Green Day - 21 guns";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "21 guns").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Green Day").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Stray heart";
            s.WithBacking = true;
            s.DisplayTitle = "Green Day - Stray heart";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Stray heart").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Green Day").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Niebieska piosenka";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Grzegorz Tomczak - Niebieska piosenka";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Niebieska piosenka").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPn1;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Grzegorz Tomczak").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Ballada Boa";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Gustavvo - Ballada Boa";
            s.TextLanguage = Enums.LanguageEnum.portugalski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Ballada Boa").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Gusttavo").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Hej sokoły!";
            s.WithBacking = true;
            s.DisplayTitle = "Hej sokoły";
            s.AuthorInTitle = false;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Hej sokoły!").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdBip;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Biesiadne").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Razem a jednak osobno";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Ich troje - Razem a jednak osobno";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Razem a jednak osobno").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Ich troje").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Zawsze z Tobą chciałbym być";
            s.WithBacking = true;
            s.DisplayTitle = "Ich troje - Zawsze z Tobą chciałbym być";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Zawsze z Tobą chciałbym być").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Ich troje").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Kolorowe jarmarki";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Janusz Laskowski - Kolorowe jarmarki";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Kolorowe jarmarki").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Janusz Laskowski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Siedem dziewcząt z Albatrosa";
            s.WithBacking = true;
            s.DisplayTitle = "Janusz Laskowski - Siedem dziewcząt z Albatrosa";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            s.Info = "Beata";
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Siedem dziewcząt z Albatrosa").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Janusz Laskowski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Bo z dziewczynami";
            s.WithBacking = true;
            s.DisplayTitle = "Jerzy Połomski - Bo z dziewczynami";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Bo z dziewczynami").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jerzy Połomski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Będziesz moja";
            s.WithBacking = true;
            s.DisplayTitle = "Jorrgus - Będziesz moja";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Będziesz moja").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jorrgus").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Intryguj, uwodź, prowokuj mnie";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Jorrgus - Intryguj, uwodź, prowokuj mnie";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Intryguj, uwodź, prowokuj mnie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jorrgus").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Ja chcę mieć żonę";
            s.WithBacking = true;
            s.DisplayTitle = "Jorrgus - Ja chcę mieć żonę";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Ja chcę mieć żonę").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jorrgus").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Kiedy patrzę na Nią";
            s.WithBacking = true;
            s.DisplayTitle = "Jorrgus - Kiedy patrzę na Nią";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Kiedy patrzę na Nią").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jorrgus").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Mama Ci mówiła";
            s.WithBacking = true;
            s.DisplayTitle = "Jorrgus - Mama Ci mówiła";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Mama Ci mówiła").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jorrgus").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Niebezpieczna";
            s.WithBacking = true;
            s.DisplayTitle = "Jorrgus - Niebezpieczna";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Niebezpieczna").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jorrgus").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Piękna nieznajoma";
            s.WithBacking = true;
            s.DisplayTitle = "Jorrgus - Piękna nieznajoma";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Piękna nieznajoma").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Jorrgus").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Black and white";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Kombi - Black and white";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Black and white").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Kombi").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Słodkiego miłego życia";
            s.WithBacking = true;
            s.DisplayTitle = "Kombi - Słodkiego miłego życia";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Słodkiego miłego życia").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Kombi").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Chciałem być";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Krzysztof Krawczyk - Chciałem być";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Chciałem być").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Krzysztof Krawczyk").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Zatańczysz ze mną";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Krzysztof Krawczyk - Zatańczysz ze mną";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Zatańczysz ze mną").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Krzysztof Krawczyk").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Zawsze tam, gdzie Ty";
            s.WithBacking = true;
            s.DisplayTitle = "Lady Pank - Zawsze tam, gdzie Ty";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Zawsze tam, gdzie Ty").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Lady Pank").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Hello";
            s.WithBacking = true;
            s.DisplayTitle = "Lionel Richie - Hello";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Hello").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Lionel Richie").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Say You Say Me";
            s.WithBacking = true;
            s.DisplayTitle = "Lionel Richie - Say You Say Me";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Say You Say Me").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Lionel Richie").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Dwudziestolatki";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Maciej Kossowski - Dwudziestolatki";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Dwudziestolatki").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Maciej Kossowski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Zuza";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Mamzel - Zuza";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Zuza").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mamzel").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Dni, których nie znamy";
            s.WithBacking = true;
            s.DisplayTitle = "Marek Grechuta - Dni, których nie znamy";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Dni, których nie znamy").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Marek Grechuta").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Moja dumka";
            s.WithBacking = true;
            s.DisplayTitle = "Marek Tranda - Moja dumka";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Moja Dumka").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Marek Tranda").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Bella";
            s.DisplayTitle = "Masters - Bella";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Bella").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Masters").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Szukam dziewczyny";
            s.WithBacking = true;
            s.DisplayTitle = "Masters - Szukam dziewczyny";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Szukam dziewczyny").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Masters").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Żono moja";
            s.WithBacking = true;
            s.DisplayTitle = "Masters - Żono moja";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Żono moja").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Masters").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Ai se ue te pego";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.DisplayTitle = "Michel Telo - Ai se ue te pego";
            s.TextLanguage = Enums.LanguageEnum.portugalski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Ai se ue te pego").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Michel Telo").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Będę przy Tobie";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Mig - Będę przy Tobie";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Będę przy Tobie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Dziewczyna z sąsiedniej ulicy";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Mig - Dziewczyna z sąsiednej ulicy";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Dziewczyna z sąsiedniej ulicy").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Jej dotyk";
            s.WithBacking = true;
            s.DisplayTitle = "Mig - Jej dotyk";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Jej dotyk").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Lalunia";
            s.WithBacking = true;
            s.DisplayTitle = "Mig - Lalunia";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Lalunia").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();






            s = new Song();
            s.Title = "Miód malina";
            s.WithBacking = true;
            s.DisplayTitle = "Mig - Miód malina";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Miód malina").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Nie ma mocnych na Mariolę";
            s.WithBacking = true;
            s.DisplayTitle = "Mig - Nie ma mocnych na Mariolę";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Nie ma mocnych na Mariolę").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Ona jedyna";
            s.WithBacking = true;
            s.DisplayTitle = "Mig - Ona jedyna";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Ona jedyna").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Słodka wariatka";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Mig - Słodka wariatka";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Słodka wariatka").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Ta malutka blondynka";
            s.WithBacking = true;
            s.DisplayTitle = "Mig - Ta malutka blondynka";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Ta malutka blondynka").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Wymarzona";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Mig - Wymarzona";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Wymarzona").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mig").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Brother Louie";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Modern Talking - Brother Louie";
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Brother Louie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Modern Talking").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Chery chery Lady";
            s.WithBacking = true;
            s.DisplayTitle = "Modern Talking - Chery chery Lady";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Chery chery Lady").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Modern Talking").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "You're my heart You're my soul";
            s.WithBacking = true;
            s.DisplayTitle = "Modern Talking - You're my heart You're my soul";
            s.AuthorInTitle = true;
            s.Info = "jo ma ha jo ma so";
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "You're my heart You're my soul").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Modern Talking").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Mario Magdaleno";
            s.WithBacking = true;
            s.AuthorInTitle = true;
            s.DisplayTitle = "Nazir - Mario Magdaleno";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Mario Magdaleno").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Nazir").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Całuj mnie";
            s.WithBacking = true;
            s.DisplayTitle = "Paweł Kukis & Piersi - Całuj mnie";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Całuj mnie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Paweł Kukiz").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = false;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Piersi").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Niewiara";
            s.DisplayTitle = "Piękni i młodzi - Niewiara";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Niewiara").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Piękni i Młodzi").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Bałkanica";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Piersi - Bałkanica";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Bałkanica").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Piersi").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Konstelacje";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Pin - Konstelacje";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Konstelacje").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Pin").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Goniąc kormorany";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Piotr Szczepanik - Goniąc kormorany";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Goniąc kormorany").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Piotr Szczepanik").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Kochać, jak to łatwo powiedzieć";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Piotr Szczepanik - Kochać, jak to łatwo powiedzieć";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Kochać, jak to łatwo powiedzieć").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Piotr Szczepanik").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Byłaś dla mnie wszystkim";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Poparzeni Kawą Trzy - Byłaś dla mnie wszystkim";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Byłaś dla mnie wszystkim").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Poparzeni Kawą Trzy").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.AuthorInTitle = true;
            s.Title = "Kawałek do tańca";
            s.WithBacking = true;
            s.DisplayTitle = "Poparzeni Kawą Trzy - Kawałek do tańca";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Kawałek do tańca").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Poparzeni Kawą Trzy").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.AuthorInTitle = true;
            s.Title = "Okrutna, zła i podła";
            s.DisplayTitle = "Poparzeni Kawą Trzy - Okrutna, zła i podła";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Okrutna, zła i podła").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Poparzeni Kawą Trzy").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Wezmę Cię";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Poparzeni Kawą Trzy - Wezmę Cię";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Wezmę Cię").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Poparzeni Kawą Trzy").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Oczy szmaragdowe";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Rezonans - Oczy szmaragdowe";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Oczy szmaragdowe").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Rezonans").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Stay";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Rihanna & Mikky Ekko - Stay";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Stay").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Rihanna").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = false;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Mikky Ekko").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Każdy swoje 10 minut ma";
            s.WithBacking = true;
            s.DisplayTitle = "Seweryn Krajewski - Każdy swoje 10 minut ma";
            s.AuthorInTitle = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Każdy swoje 10 minut ma").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPn1;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Seweryn Krajewski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Nagroda za odwagę";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Seweryn Krajewski - Nagroda za odwagę";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Nagroda za odwagę").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Seweryn Krajewski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Najpiękniejsza";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.DisplayTitle = "Seweryn Krajewski - Najpiękniejsza";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Najpiękniejsza").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Seweryn Krajewski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Nie mówmy o zmartwieniach";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Seweryn Krajewski - Nie mówmy o zmartwieniach";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Nie mówmy o zmartwieniach").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Seweryn Krajewski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Uciekaj moje serce";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Seweryn Krajewski - Uciekaj moje serce";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Uciekaj moje serce").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Seweryn Krajewski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();





            s = new Song();
            s.Title = "Wielka miłość";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.DisplayTitle = "Seweryn Krajewski - Wielka miłość";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Wielka miłość").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdWol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPn1;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Seweryn Krajewski").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Give me your heart tonight";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.DisplayTitle = "Shakin' Stevens - Give me your heart tonight";
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Give me your heart tonight").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Shakin' Stevens").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Marie Marie";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Shakin' Stevens - Marie Marie";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Marie Marie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Shakin' Stevens").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Oh Julie";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Shakin' Stevens - Oh Julie";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Oh Julie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Shakin' Stevens").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "This Ole House";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Shakin' Stevens - This Ole House";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "This Ole House").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Shakin' Stevens").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "You drive me crazy";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Shakin' Stevens - You drive me crazy";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "You drive me crazy").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Shakin' Stevens").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Może ze mną zatańczysz";
            s.DisplayTitle = "Shantel - Może ze mną zatańczysz";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Może ze mną zatańczysz").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Shantel").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Prześliczna wiolonczelistka";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.DisplayTitle = "Skaldowie - Prześliczna wiolonczelistka";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Prześliczna wiolonczelistka").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Skaldowie").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Wierniejsza od marzenia";
            s.AuthorInTitle = true;
            s.WithBacking = true;
            s.DisplayTitle = "Skaldowie - Wierniejsza od marzenia";
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Wierniejsza od marzenia").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Skaldowie").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Aneta";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Sławomir - Aneta";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Aneta").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Sławomir").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Miłość w Zakopanem";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Sławomir - Miłość w Zakopanem";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Miłość w Zakopanem").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdRck;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Sławomir").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();



            s = new Song();
            s.Title = "Sex Bomb";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Tom Jones - Sex Bomb";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Sex Bomb").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Tom Jones").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "YMCA";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Village People - YMCA";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.angielski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "YMCA").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdZag;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Village People").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Ona tańczy dla mnie";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Weekend - Ona tańczy dla mnie";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Ona tańczy dla mnie").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdDis;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Weekend").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();




            s = new Song();
            s.Title = "Baśka";
            s.AuthorInTitle = true;
            s.DisplayTitle = "Wilki - Baśka";
            s.WithBacking = true;
            s.TextLanguage = Enums.LanguageEnum.polski;
            context.Songs.Add(s);
            context.SaveChanges();
            songID = context.Songs.Where(x => x.Title == "Baśka").Select(y => y.SongID).First();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPol;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sc = new SongCategory();
            sc.SongID = songID;
            sc.CategoryID = catIdPop;
            context.SongCategories.Add(sc);
            context.SaveChanges();

            sa = new SongAuthor();
            sa.SongID = songID;
            sa.MainSongAuthor = true;
            sa.AuthorID = context.Authors.Where(x => x.Name == "Wilki").Select(y => y.AuthorID).First();
            context.SongAuthors.Add(sa);
            context.SaveChanges();

            #endregion

            #region backings
            int sid;
            Backing b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Biorę urlop od Ciebie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Biorę urlop od Ciebie INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - Biorę urlop od Ciebie INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Czekam na Ciebie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Czekam na Ciebie INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - Czekam na Ciebie INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Kochana wierzę w miłość").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Kochana wierzę w miłość INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - Kochana wierzę w miłość INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Królowa nocy").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Królowa nocy INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - Królowa nocy INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Prawdziwa miłość to Ty").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Prawdziwa miłość to Ty INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - Prawdziwa miłość to Ty INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Przekorny los").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Przekorny los INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - Przekorny los INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Przez Twe oczy zielone").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Przez Twe oczy zielone INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - Przez Twe oczy zielone INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Sonet dla miłości").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Sonet dla miłości INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Akcent - Sonet dla miłości INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Taką Cię Wyśniłem").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Taką Cię Wyśniłem INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Akcent - Taką Cię Wyśniłem INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "W sercu mi graj").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - W sercu mi graj INSTRUMENTAL.mp3";
            b.Path = "/Music/Akcent - W sercu mi graj INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Życie to są chwile").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Akcent - Życie to są chwile INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Akcent - Życie to są chwile INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Chodź, Przytul, Przebacz").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Andrzej Piaseczny - Chodź, Przytul, Przebacz INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Andrzej Piaseczny - Chodź, Przytul, Przebacz INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Nie liczę godzin i lat").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Andrzej Rybinski - Nie liczę godzin i lat INSTRUMENTAL.mp3";
            b.Path = "/Music/Andrzej Rybinski - Nie liczę godzin i lat INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Moje jedyne marzenie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Anna Jantar - Moje jedyne marzenie INSTRUMENTAL ^2 Tomek.mp3";
            b.Path = "/Music/Anna Jantar - Moje jedyne marzenie INSTRUMENTAL ^2 Tomek.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Moje jedyne marzenie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Anna Jantar - Moje jedyne marzenie INSTRUMENTAL v1 Monika.mp3";
            b.Path = "/Music/Anna Jantar - Moje jedyne marzenie INSTRUMENTAL v1 Monika.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Bogini niepojęta").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Antek Mojkowski - Bogini niepojęta INSTRUMENTAL.mp3";
            b.Path = "/Music/Antek Mojkowski - Bogini niepojęta INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "No 1. Parthy anthem").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Arctic Monkeys - No.1 Party anthem INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Arctic Monkeys - No.1 Party anthem INSTRUMENTAL ^.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();


            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "You're a woman, I'm a man").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Bad Boys Blue - You're a woman, I'm a man INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Bad Boys Blue - You're a woman, I'm a man INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Cyganeczka Zosia").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Cyganeczka Zosia INSTRUMENTAL.mp3";
            b.Path = "/Music/Cyganeczka Zosia INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Rudy się żeni").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Big Cyc - Rudy się żeni INSTRUMENTAL.mp3";
            b.Path = "/Music/Big Cyc - Rudy się żeni INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Bombonierka").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Bombonierka INSTRUMENTAL.mp3";
            b.Path = "/Music/Bombonierka INSTRUMENTALL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Moja Kochana").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Boys - Moja Kochana INSTRUMENTAL.mp3";
            b.Path = "/Music/Boys - Moja Kochana INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Za szkłem").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Bracia - Za szkłem INSTRUMENTAL v3.mp3";
            b.Path = "/Music/Bracia - Za szkłem INSTRUMENTAL v3.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Let's Twist Again").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Chubby Checker - Let's Twist Again INSTRUMENTAL v2 MONIKA.mp3";
            b.Path = "/Music/Chubby Checker - Let's Twist Again INSTRUMENTAL v2 MONIKA.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Let's Twist Again").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Chubby Checker - Let's Twist Again INSTRUMENTAL BETTER v5 Tomek.mp3";
            b.Path = "/Music/Chubby Checker - Let's Twist Again INSTRUMENTAL BETTER v5 Tomek.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Moje ciało oszalało").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Cliver - Moje cialo oszalało INSTRUMENTAL.mp3";
            b.Path = "/Music/Cliver - Moje cialo oszalało INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Wezmę Cię ze sobą").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Czerwone Gitary - Wezmę Cię ze sobą INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Czerwone Gitary - Wezmę Cię ze sobą INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "This Year's Love").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "David Gray - This Year's Love INSTRUMENTAL v1.mp3";
            b.Path = "/Music/David Gray - This Year's Love INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Zakochane Oczy").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Defis & Marcin Miller - Zakochane Oczy INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Defis & Marcin Miller - Zakochane Oczy INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Dumka na dwa serca").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Dumka na dwa serca INSTRUMENTAL.mp3";
            b.Path = "/Music/Dumka na dwa serca INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Do kołyski").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Dżem - Do kołyski INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Dżem - Do kołyski INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "September").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Earth, Wind and Fire - September INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Earth, Wind and Fire - September INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = true;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Always on my mind").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Elvis Presley - Always on my mind INSTRUMENTAL.mp3";
            b.Path = "/Music/Elvis Presley - Always on my mind INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Burning Love").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Elvis Presley - Burning Love INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Elvis Presley - Burning Love INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Can't help falling in love").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Elvis Presley - Can't help falling in love INSTRUMENTAL BETTER ^1.mp3";
            b.Path = "/Music/Elvis Presley - Can't help falling in love INSTRUMENTAL BETTER ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Kamień z napisem Love").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Enej - Kamień z napisem Love INSTRUMENTAL v3.mp3";
            b.Path = "/Music/Enej - Kamień z napisem Love INSTRUMENTAL v3.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Skrzydlate ręce").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Enej - Skrzydlate ręce INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Enej - Skrzydlate ręce INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Flames of love").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Fancy - Flames of love INSTRUMENTAL.mp3";
            b.Path = "/Music/Fancy - Flames of love INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "My way").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Frank Sinatra - My way INSTRUMENTAL.mp3";
            b.Path = "/Music/Frank Sinatra - My way INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Uno Paloma Blanca").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "George Baker - Uno Paloma Blanca INSTRUMENTAL.mp3";
            b.Path = "/Music/George Baker - Uno Paloma Blanca INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Pędzą konie po betonie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Golec Uorkiestra - Pędzą konie po betonie INSTRUMENTAL.mp3";
            b.Path = "/Music/Golec Uorkiestra - Pędzą konie po betonie INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Pieniądze to nie wszystko").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Golec Uorkiestra - Pieniądze to nie wszystko INSTRUMENTAL.mp3";
            b.Path = "/Music/Golec Uorkiestra - Pieniądze to nie wszystko INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Ściernisko").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Golec Uorkiestra - Ściernisko INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Golec Uorkiestra - Ściernisko INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Słodycze").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Golec Uorkiestra - Słodycze INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Golec Uorkiestra - Słodycze INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Życie jest muzyką").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Golec Uorkiestra - Życie jest muzyką INSTRUMNETAL.mp3";
            b.Path = "/Music/Golec Uorkiestra - Życie jest muzyką INSTRUMNETAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "21 guns").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Green Day - 21 guns INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Green Day - 21 guns INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Stray heart").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Green Day - Stray heart INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Green Day - Stray heart INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Niebieska piosenka").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Grzegorz Tomczak - Niebieska piosenka INSTRUMENTAL.mp3";
            b.Path = "/Music/Grzegorz Tomczak - Niebieska piosenka INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Ballada Boa").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Gusttavo - Ballada Boa INSTRUMENTAL.mp3";
            b.Path = "/Music/Gusttavo - Ballada Boa INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Razem a jednak osobno").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Ich troje - Razem a jednak osobno INSTRUMENTAL.mp3";
            b.Path = "/Music/Ich troje - Razem a jednak osobno INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Zawsze z Tobą chciałbym być").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Ich troje - Zawsze z Tobą chciałbym być INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Ich troje - Zawsze z Tobą chciałbym być INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Kolorowe jarmarki").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Janusz Laskowski - Kolorowe jarmarki INSTRUMENTAL laskowski ^5 Tomek.mp3";
            b.Path = "/Music/Janusz Laskowski - Kolorowe jarmarki INSTRUMENTAL laskowski ^5 Tomek.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Siedem dziewcząt z Albatrosa").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Janusz Laskowski - Siedem dziewcząt z Albatrosa  INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Janusz Laskowski - Siedem dziewcząt z Albatrosa  INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Bo z dziewczynami").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jerzy Polomski - Bo z dziewczynami INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Jerzy Polomski - Bo z dziewczynami INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Będziesz moja").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jorrgus - Będziesz moja INSTRUMENTAL.mp3";
            b.Path = "/Music/Jorrgus - Będziesz moja INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Intryguj, uwodź, prowokuj mnie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jorrgus - Intryguj uwodź prowokuj mnie INSTRUMENTAL ^2.mp3";
            b.Path = "/Music/Jorrgus - Intryguj uwodź prowokuj mnie INSTRUMENTAL ^2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Ja chcę mieć żonę").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jorrgus - Ja chcę mieć żonę INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Jorrgus - Ja chcę mieć żonę INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Kiedy patrzę na Nią").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jorrgus - Kiedy patrzę na Nią INSTRUMENTAL.mp3";
            b.Path = "/Music/Jorrgus - Kiedy patrzę na Nią INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Mama ci mówiła").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jorrgus - Mama ci mówiła INSTRUMENTAL.mp3";
            b.Path = "/Music/Jorrgus - Mama ci mówiła INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Niebezpieczna").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jorrgus - Niebezpieczna INSTRUMENTAL.mp3";
            b.Path = "/Music/Jorrgus - Niebezpieczna INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Piękna Nieznajoma").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Jorrgus - Piękna Nieznajoma INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Jorrgus - Piękna Nieznajoma INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Black and white").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Kombi - Black nad white INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Kombi - Black nad white INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Słodkiego miłego życia").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Kombi - Słodkiego miłego życia INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Kombi - Słodkiego miłego życia INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Chciałem być").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Krzysztof Krawczyk - Chciałem być INSTRUMENTAL  ^2.mp3";
            b.Path = "/Music/Krzysztof Krawczyk - Chciałem być INSTRUMENTAL  ^2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Zatańczysz ze mną").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Krzysztof Krawczyk - Zatańczysz ze mną INSTRUMENTAL.mp3";
            b.Path = "/Music/Krzysztof Krawczyk - Zatańczysz ze mną INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Zawsze tam, gdzie Ty").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Lady Pank - Zawsze tam, gdzie Ty INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Lady Pank - Zawsze tam, gdzie Ty INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Hello").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Lionel Richie - Hello INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Lionel Richie - Hello INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Dwudziestolatki").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Maciej Kossowski - Dwudziestolatki INSTRUMENTAL.mp3";
            b.Path = "/Music/Maciej Kossowski - Dwudziestolatki INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Zuza").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mamzel - Zuza INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mamzel - Zuza INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Dni, których nie znamy").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Marek Grechuta - Dni, których  nie znamy.mp3";
            b.Path = "/Music/Marek Grechuta - Dni, których  nie znamy.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Moja dumka").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Marek Tranda - Moja dumka INSTRUMENATL v2.mp3";
            b.Path = "/Music/Marek Tranda - Moja dumka INSTRUMENATL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Żono moja").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Masters - Żono moja INSTRUMENTAL.mp3";
            b.Path = "/Music/Masters - Żono moja INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Ai se ue te pego").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Michel Telo - Ai se ue te pego INSTRUMENTAL.mp3";
            b.Path = "/Music/Michel Telo - Ai se ue te pego INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Będę przy Tobie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Będę przy Tobie INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mig - Będę przy Tobie INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Dziewczyna z sąsiedniej ulicy").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Dziewczyna z sąsiedniej ulicy INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mig - Dziewczyna z sąsiedniej ulicy INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Jej dotyk").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Jej dotyk INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mig - Jej dotyk INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Lalunia").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Lalunia INSTRUMENTAL.mp3";
            b.Path = "/Music/Mig - Lalunia INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Miód malina").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Miód malina  INSTRUMENATL v1.mp3";
            b.Path = "/Music/Mig - Miód malina  INSTRUMENATL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Nie ma mocnych na Mariolę").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Nie ma mocnych na Mariolę INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mig - Nie ma mocnych na Mariolę INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Ona jedyna").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Ona jedyna INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mig - Ona jedyna INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Słodka wariatka").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Słodka wariatka INSTRUMENTAL.mp3";
            b.Path = "/Music/Mig - Słodka wariatka INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Ta malutka blondynka").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Ta malutka blondynka INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mig - Ta malutka blondynka INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Wymarzona").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Mig - Wymarzona INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Mig - Wymarzona INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Mario Magdaleno").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Nazir - Mario Magdaleno INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Nazir - Mario Magdaleno INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Niewiara").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Piękni i Młodzi - Niewiara INSTRUMENTAL  v2 Monika.mp3";
            b.Path = "/Music/Piękni i Młodzi - Niewiara INSTRUMENTAL  v2 Monika.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Bałkanica").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Piersi - Bałkanica INSTRUMENTAL v3 Tomek.mp3";
            b.Path = "/Music/Piersi - Bałkanica INSTRUMENTAL v3 Tomek.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Konstelacje").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Pin - Konstelacje INSTRUMENTAL.mp3";
            b.Path = "/Music/Pin - Konstelacje INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Kochać, jak to łatwo powiedzieć").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Piotr Szczepanik - Kochać, jak to łatwo powiedzieć INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Piotr Szczepanik - Kochać, jak to łatwo powiedzieć INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Goniąc Kormorany").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Piotr Szczepanik - Goniąc Kormorany INSTRUMENTAL ^2.mp3";
            b.Path = "/Music/Piotr Szczepanik - Goniąc Kormorany INSTRUMENTAL ^2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Byłaś dla mnie wszystkim").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Poparzeni Kawą Trzy - Byłaś dla mnie wszystkim INSTRUMENTAL ^2.mp3";
            b.Path = "/Music/Poparzeni Kawą Trzy - Byłaś dla mnie wszystkim INSTRUMENTAL ^2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Kawałek do tańca").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Poparzeni Kawą Trzy - Kawałek do tańca INSTRUMENTAL.mp3";
            b.Path = "/Music/Poparzeni Kawą Trzy - Kawałek do tańca INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Okrutna, zła i podła").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Poparzeni Kawą Trzy - Okrutna, zła i podła INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Poparzeni Kawą Trzy - Okrutna, zła i podła INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Wezmę Cię").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Poparzeni Kawą Trzy - Wezmę Cię INSTRUMENTAL ^2.mp3";
            b.Path = "/Music/Poparzeni Kawą Trzy - Wezmę Cię INSTRUMENTAL ^2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Okrutna, zła i podła").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Poparzeni Kawą Trzy - Okrutna, zła i podła INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Poparzeni Kawą Trzy - Okrutna, zła i podła INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Wezmę Cię").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Poparzeni Kawą Trzy - Wezmę Cię INSTRUMENTAL ^2.mp3";
            b.Path = "/Music/Poparzeni Kawą Trzy - Wezmę Cię INSTRUMENTAL ^2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Oczy szmaragdowe").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Rezonans - Oczy szmaragdowe INSTRUMENTAL.mp3";
            b.Path = "/Music/Rezonans - Oczy szmaragdowe INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Każdy swoje 10 minut ma").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Każdy swoje 10 minut ma INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Seweryn Krajewski - Każdy swoje 10 minut ma INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Nagroda za odwagę").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Nagroda za odwagę INSTRUMENTAL v1.mp3";
            b.Path = "/Music/Seweryn Krajewski - Nagroda za odwagę INSTRUMENTAL v1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Najpiękniejsza").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Najpiękniejsza INSTRUMENTAL.mp3";
            b.Path = "/Music/Seweryn Krajewski - Najpiękniejsza INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Nie mówmy o zmartwieniach").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Nie mówmy o zmartwieniach INSTRUMENTAL.mp3";
            b.Path = "/Music/Seweryn Krajewski - Nie mówmy o zmartwieniach INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Uciekaj moje serce").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Uciekaj moje serce INSTRUMENTAL.mp3";
            b.Path = "/Music/Seweryn Krajewski - Uciekaj moje serce INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Wielka miłość").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Wielka miłość INSTRUMENTAL.mp3";
            b.Path = "/Music/Seweryn Krajewski - Wielka miłość INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Uciekaj moje serce").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Uciekaj moje serce INSTRUMENTAL.mp3";
            b.Path = "/Music/Seweryn Krajewski - Uciekaj moje serce INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Wielka miłość").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Seweryn Krajewski - Wielka miłość INSTRUMENTAL.mp3";
            b.Path = "/Music/Seweryn Krajewski - Wielka miłość INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Give me your heart tonight").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Shakin Stevens - Give me your heart tonight INSTRUMENTAL v2.mp3";
            b.Path = "/Music/Shakin Stevens - Give me your heart tonight INSTRUMENTAL v2.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Może ze mną zatańczysz").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Shantel - Może ze mną zatańczysz INSTRUMENTAL.mp3";
            b.Path = "/Music/Shantel - Może ze mną zatańczysz INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Prześliczna wiolonczelistka").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Skaldowie - Prześliczna wiolonczelistka INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Skaldowie - Prześliczna wiolonczelistka INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Wierniejsza od marzenia").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Skaldowie - Wierniejsza od marzenia INSTRUMENTAL.mp3";
            b.Path = "/Music/Skaldowie - Wierniejsza od marzenia INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Aneta").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Sławomir - Aneta INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Sławomir - Aneta INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Miłość w Zakopanem").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Sławomir - Miłość w Zakopanem INSTRUMENTAL.mp3";
            b.Path = "/Music/Sławomir - Miłość w Zakopanem INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "YMCA").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Village People - YMCA INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Village People - YMCA INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Ona tańczy dla mnie").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Weekend - Ona tańczy dla mnie INSTRUMENTAL ^1.mp3";
            b.Path = "/Music/Weekend - Ona tańczy dla mnie INSTRUMENTAL ^1.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();

            b = new Backing();
            sid = context.Songs.Where(x => x.Title == "Baśka").Select(y => y.SongID).First();
            b.BackingStatus = Enums.BackingStatusEnum.Good;
            b.Name = "Wilki - Baśka INSTRUMENTAL.mp3";
            b.Path = "/Music/Wilki - Baśka INSTRUMENTAL.mp3";
            b.SongID = sid;
            b.MainBacking = false;
            context.Backings.Add(b);
            context.SaveChanges();
            #endregion



            #region place

            Place plc = new Place();
            plc.Name = "Remiza w Jaziewie";
            plc.Address = "Jaziewo 47, podlaskie, gmina sztabin";
            plc.Description = "dość kameralna sala główna, kominek, fajna sala z bilardem i kanapami, zadbana, po remoncie, toaleta, kuchnia, miejsce na ognisko";
            context.Places.Add(plc);
            context.SaveChanges();

            plc = new Place();
            plc.Name = "EmptyPlace";
            plc.Address = "empty";
            plc.Description = "empty";
            context.Places.Add(plc);
            context.SaveChanges();

            plc = new Place();
            plc.Name = "Sala Weselna Łapińscy";
            plc.Address = "Stare Kupiski, ul.Janowska 1, Łomża";
            context.Places.Add(plc);
            context.SaveChanges();

            Event e = new Event();
            e.Date = new DateTime(2017, 12, 31, 19, 00, 0);
            e.DateEnd = new DateTime(2018, 01, 01, 04, 00, 0);
            e.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            e.Title = "Sylwester 2017";
            e.Info = "Pierwsza konkretna impreza TYTANIKA w składzie Aneta, Monika, Tomek.";
            e.EventType = Enums.EventType.granie;
            ids = context.Users.Where(n => n.UserName == "Tomek").Select(n => n.Id).FirstOrDefault();
            e.UserID = ids;
            context.Events.Add(e);
            context.SaveChanges();

            e = new Event();
            e.Date = new DateTime(2018, 01, 04, 19, 00, 0);
            e.DateEnd = new DateTime(2018, 01, 05, 04, 00, 0);

            e.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            e.Title = "Sylwester 2017";
            e.Info = "Pierwsza konkretna impreza TYTANIKA w składzie Aneta, Monika, Tomek.";
            e.EventType = Enums.EventType.granie;
            ids = context.Users.Where(n => n.UserName == "Tomek").Select(n => n.Id).FirstOrDefault();
            e.UserID = ids;
            context.Events.Add(e);
            context.SaveChanges();

            e = new Event();
            e.Date = new DateTime(2018, 01, 03, 19, 00, 0);
            e.DateEnd = new DateTime(2018, 01, 04, 04, 00, 0);
            e.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            e.Title = "Sylwester 2017";
            e.Info = "Pierwsza konkretna impreza TYTANIKA w składzie Aneta, Monika, Tomek.";
            e.EventType = Enums.EventType.granie;
            ids = context.Users.Where(n => n.UserName == "Tomek").Select(n => n.Id).FirstOrDefault();
            e.UserID = ids;
            context.Events.Add(e);
            context.SaveChanges();

            e = new Event();
            e.Date = new DateTime(2018, 01, 02, 19, 00, 0);
            e.DateEnd = new DateTime(2018, 01, 03, 04, 00, 0);
            e.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            e.Title = "Sylwester 2017";
            e.Info = "Pierwsza konkretna impreza TYTANIKA w składzie Aneta, Monika, Tomek.";
            e.EventType = Enums.EventType.granie;
            ids = context.Users.Where(n => n.UserName == "Tomek").Select(n => n.Id).FirstOrDefault();
            e.UserID = ids;
            context.Events.Add(e);
            context.SaveChanges();

            e = new Event();
            e.Date = new DateTime(2018, 01, 01, 19, 00, 0);
            e.DateEnd = new DateTime(2018, 01, 02, 04, 00, 0);
            e.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            e.Title = "Sylwester 2017";
            e.Info = "Pierwsza konkretna impreza TYTANIKA w składzie Aneta, Monika, Tomek.";
            e.EventType = Enums.EventType.granie;
            ids = context.Users.Where(n => n.UserName == "Tomek").Select(n => n.Id).FirstOrDefault();
            e.UserID = ids;
            context.Events.Add(e);
            context.SaveChanges();

            e = new Event();
            e.Date = new DateTime(2018, 05, 12, 13, 00, 0);
            e.DateEnd = new DateTime(2018, 05, 13, 04, 00, 0);
            e.PlaceID = context.Places.Where(x => x.Name == "Sala Weselna Łapińscy").Select(y => y.PlaceID).FirstOrDefault();
            e.Title = "Wesele";
            e.Info = "Wesele, młodzi życzą sobie repertuar z ogrnaiczoną ilością discopolo";
            e.EventType = Enums.EventType.granie;
            ids = context.Users.Where(n => n.UserName == "Tomek").Select(n => n.Id).FirstOrDefault();
            e.UserID = ids;
            context.Events.Add(e);
            context.SaveChanges();

            Photo photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/remizaJ.PNG";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/dwl.PNG";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos1.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos2.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos3.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos4.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos5.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos6.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos7.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos8.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos9.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos10.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos11.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos12.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos13.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos14.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos15.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos16.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos17.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos18.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos19.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2016, 02, 01, 00, 00, 0);
            photo.Path = "/images/Gallery/partyPhotos20.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2018, 01, 01, 03, 30, 0);
            photo.Path = "/images/Gallery/md.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            photo = new Photo();
            photo.DateCreated = new DateTime(2017, 12, 31, 22, 00, 0);
            photo.Path = "/images/Gallery/sylw2.jpg";
            context.Photos.Add(photo);
            context.SaveChanges();

            PlacePhoto pp = new PlacePhoto();
            pp.PhotoID = context.Photos.Where(x => x.Path == "/images/Gallery/remizaJ.PNG").Select(y => y.PhotoID).FirstOrDefault();
            pp.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            context.PlacePhotos.Add(pp);
            context.SaveChanges();

            pp = new PlacePhoto();
            pp.PhotoID = context.Photos.Where(x => x.Path == "/images/Gallery/dwl.PNG").Select(y => y.PhotoID).FirstOrDefault();
            pp.PlaceID = context.Places.Where(x => x.Name == "Sala Weselna Łapińscy").Select(y => y.PlaceID).FirstOrDefault();
            context.PlacePhotos.Add(pp);
            context.SaveChanges();

            pp = new PlacePhoto();
            pp.PhotoID = context.Photos.Where(x => x.Path == "/images/Gallery/sylw2.jpg").Select(y => y.PhotoID).FirstOrDefault();
            pp.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            context.PlacePhotos.Add(pp);
            context.SaveChanges();

            pp = new PlacePhoto();
            pp.PhotoID = context.Photos.Where(x => x.Path == "/images/Gallery/md.jpg").Select(y => y.PhotoID).FirstOrDefault();
            pp.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            context.PlacePhotos.Add(pp);
            context.SaveChanges();

            pp = new PlacePhoto();
            pp.PhotoID = context.Photos.Where(x => x.Path == "/images/Gallery/partyPhotos16.jpg").Select(y => y.PhotoID).FirstOrDefault();
            pp.PlaceID = context.Places.Where(x => x.Name == "Remiza w Jaziewie").Select(y => y.PlaceID).FirstOrDefault();
            context.PlacePhotos.Add(pp);
            context.SaveChanges();

            #endregion
        }

        private static void DeleteAllRecords(ApplicationDbContext context)
        {
            context.Authors.RemoveRange(context.Authors);
            context.SaveChanges();
            context.Backings.RemoveRange(context.Backings);
            context.SaveChanges();
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();
            context.Events.RemoveRange(context.Events);
            context.SaveChanges();
            context.Photos.RemoveRange(context.Photos);
            context.SaveChanges();
            context.PlacePhotos.RemoveRange(context.PlacePhotos);
            context.SaveChanges();
            context.Places.RemoveRange(context.Places);
            context.SaveChanges();
            context.Posts.RemoveRange(context.Posts);
            context.SaveChanges();
            context.SongAuthors.RemoveRange(context.SongAuthors);
            context.SaveChanges();
            context.SongCategories.RemoveRange(context.SongCategories);
            context.SaveChanges();
            context.Songs.RemoveRange(context.Songs);
            context.SaveChanges();
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }

        private static async Task HandleRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User", "BandMember" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
