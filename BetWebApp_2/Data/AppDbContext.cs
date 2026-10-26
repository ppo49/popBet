using Microsoft.EntityFrameworkCore;
using BetWebApp_2.Models;

namespace BetWebApp_2.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Pick> Picks => Set<Pick>();
        public DbSet<MyPick> MyPicks => Set<MyPick>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<User>().HasIndex(u => u.Username).IsUnique();
            b.Entity<User>().Property(u => u.Role).HasConversion<string>();

            b.Entity<Ticket>()
                .HasMany(t => t.MyPicks)
                .WithOne(m => m.Ticket)
                .HasForeignKey(m => m.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<MyPick>()
                .HasOne(m => m.Pick)
                .WithMany()
                .HasForeignKey(m => m.PickId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
