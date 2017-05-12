using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using LobbyingMadeSimple.Core;

namespace LobbyingMadeSimple.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<Contribution> Contributions { get; set; }
    }
}