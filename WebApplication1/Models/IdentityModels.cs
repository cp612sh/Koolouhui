using Microsoft.AspNet.Identity.EntityFramework;

namespace Koo.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 13位中国公民身份证号码
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNumber { get; set; }

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

        public System.Data.Entity.DbSet<Koo.Web.Areas.BBS.Models.Post> Posts { get; set; }

    }
}