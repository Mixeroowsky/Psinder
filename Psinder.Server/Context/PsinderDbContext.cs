using Microsoft.EntityFrameworkCore;
using Psinder.Server.Entities;

namespace Psinder.Server.Context
{
    public class PsinderDbContext : DbContext
    {
        public PsinderDbContext(DbContextOptions op) : base(op)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Shelter> Shelters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Shelter)
                .WithMany(p => p.Pets)
                .HasForeignKey(s => s.ShelterId)
                .IsRequired();
            modelBuilder.Entity<Shelter>()
                .HasMany(s => s.Pets)
                .WithOne(s => s.Shelter)
                .HasForeignKey(s => s.ShelterId)
                .IsRequired();
        }
    }
}
