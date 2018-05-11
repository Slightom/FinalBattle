using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalBattle.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinalBattle.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
 

        //Klasa dodana
        //public class IdentityManager
        //{
        //    public RoleManager<IdentityRole> LocalRoleManager
        //    {
        //        get
        //        {
        //            return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        //        }
        //    }


        //    public UserManager<ApplicationUser> LocalUserManager
        //    {
        //        get
        //        {
        //            return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //        }
        //    }


        //    public ApplicationUser GetUserByID(string userID)
        //    {
        //        ApplicationUser user = null;
        //        UserManager<ApplicationUser> um = this.LocalUserManager;

        //        user = um.FindById(userID);

        //        return user;
        //    }


        //    public ApplicationUser GetUserByName(string email)
        //    {
        //        ApplicationUser user = null;
        //        UserManager<ApplicationUser> um = this.LocalUserManager;

        //        user = um.FindByEmail(email);

        //        return user;
        //    }


        //    public bool RoleExists(string name)
        //    {
        //        var rm = LocalRoleManager;

        //        return rm.RoleExists(name);
        //    }

        //    public List<IdentityRole> GetRoles()
        //    {
        //        var rm = LocalRoleManager;
        //        return rm.Roles.ToList();
        //    }


        //    public bool CreateRole(string name)
        //    {
        //        var rm = LocalRoleManager;
        //        var idResult = rm.Create(new IdentityRole(name));

        //        return idResult.Succeeded;
        //    }


        //    public bool CreateUser(ApplicationUser user, string password)
        //    {
        //        var um = LocalUserManager;
        //        var idResult = um.Create(user, password);

        //        return idResult.Succeeded;
        //    }


        //    public bool AddUserToRole(string userId, string roleName)
        //    {
        //        var um = LocalUserManager;
        //        var idResult = um.AddToRole(userId, roleName);

        //        return idResult.Succeeded;
        //    }


        //    public bool AddUserToRoleByUsername(string username, string roleName)
        //    {
        //        var um = LocalUserManager;

        //        string userID = um.FindByName(username).Id;
        //        var idResult = um.AddToRole(userID, roleName);

        //        return idResult.Succeeded;
        //    }


        //    public void ClearUserRoles(string userId)
        //    {
        //        var um = LocalUserManager;
        //        var user = um.FindById(userId);
        //        var currentRoles = new List<IdentityUserRole>();

        //        currentRoles.AddRange(user.Roles);

        //        foreach (var role in currentRoles)
        //        {
        //            um.RemoveFromRole(userId, role.RoleId);
        //        }
        //    }
        //}
    }
}
