﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace Koo.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Koo.Web.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<Koo.Web.Models.SupportAmount> SupportAmounts { get; set; }

        public System.Data.Entity.DbSet<Koo.Web.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Koo.Web.Models.OrderItem> OrderItems { get; set; }

    }
}