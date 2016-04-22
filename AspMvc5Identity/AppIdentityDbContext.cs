using System.Data.Entity;
using System.Web.Mvc;
using AspMvc5Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspMvc5Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("IdentityDb"){}

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());            
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
}