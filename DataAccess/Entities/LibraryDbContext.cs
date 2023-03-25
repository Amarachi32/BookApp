using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities
{
    public class LibraryDbContext : IdentityDbContext<AppUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }


        public DbSet<Book> Books { get; set; }
 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(e =>
            {
                e.Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired();
                e.HasIndex(u => u.Email)
                 .IsUnique();
            });



            modelBuilder.Entity<AppUser>()
                .Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired();



            modelBuilder.Entity<Book>()
                       .HasOne(b => b.AppUser)
                       .WithMany(u => u.BookList)
                       .HasForeignKey(b => b.AppUserId);


            modelBuilder.Entity<Book>()
                .Property(t => t.Title)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(t => t.Description)
                .HasMaxLength(1000);


            base.OnModelCreating(modelBuilder);

        }

    }
}
