using AsrWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AsrWebApi.Data
{
    public class AsrContext : DbContext
    {
        public AsrContext(DbContextOptions<AsrContext> options) : base(options)
        { }

        public DbSet<Room> Room { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Slot> Slot { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Slot>().HasKey(x => new { x.RoomID, x.StartTime });
        }
    }
}
