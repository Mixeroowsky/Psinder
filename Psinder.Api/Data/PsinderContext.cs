using Microsoft.EntityFrameworkCore;

namespace Psinder.Api.Data
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
