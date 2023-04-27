using Microsoft.EntityFrameworkCore;

namespace Psinder.Api.Models
{
    public class PsinderContext : DbContext
    {

        public PsinderContext()
        {

        }
        public PsinderContext(DbContextOptions<PsinderContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Shelter> Shelters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Pet>().HasOne(s => s.Shelter);
            modelBuilder.Entity<Shelter>().HasMany(p => p.Pets);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Psinder;Integrated Security=true;TrustServerCertificate=True;");
            }
        }

    }

}
