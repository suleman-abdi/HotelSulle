using Microsoft.EntityFrameworkCore;

namespace  HotelSulleLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Room> Room { get; set; }

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=AbdiHotel;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

    }
}
