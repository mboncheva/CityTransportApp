using CityTransport.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CityTransport.Data
{
    public class CityTransprtDbContext : IdentityDbContext<User>
    {
        public DbSet<CustomerCard> CustomerCards { get; set; }
        public DbSet<SubscriptionCard> SubscriptionCards { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<LineStation> LineStations { get; set; }
        public DbSet<LineUser> LineUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }

        public CityTransprtDbContext(DbContextOptions<CityTransprtDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LineStation>()
               .HasKey(x => new { x.LineId, x.StationId });

            builder.Entity<LineUser>()
               .HasKey(x => new { x.LineId, x.UserId });

            builder.Entity<User>()
                .HasOne(c => c.CustomerCard)
                .WithOne(u => u.User)
                .HasForeignKey<CustomerCard>(u => u.UserId);

            base.OnModelCreating(builder);
        }
    }
}
