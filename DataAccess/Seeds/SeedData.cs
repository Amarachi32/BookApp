using DataAccess.Entities;
using DataAccess.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Seeds
{
    public class SeedData
    {
        private readonly UserManager<AppUser> _userManager;

        public SeedData(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            LibraryDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<LibraryDbContext>();

/*            var users = await _userManager.Users.ToListAsync();
            if ( _userManager.Users.ToListAsync())
            {
                await context.appUsers.AddRangeAsync(GetAuthorsWithTheirBooks());
                await context.SaveChangesAsync();
            }*/
        }

        //public static IList<AppUser> GetAuthorsWithTheirBooks()
/*        public static IList<AppUser> GetAuthorsWithTheirBooks(UserManager<AppUser> userManager)
        {
            return new List<AppUser>() {
            new AppUser()
            {
                Name= "Andrew Troeslan",
                Email= "andrew@gmail.com",
                Password = "@Andrew213",
                BookList = new List<Book>()
                {
                   new Book()
                   {
                       
                        Title = "Pro C# 10",
                        Author = "Andrew Troeslan",
                        Description = "213213",
                        type = Genre.NonFiction,
                        ISBN = "Pro C# book for Begineers",
                        price = 300,
                        AvailableCopies = 0,
                        PublishedDate = DateTime.Now.AddDays(30),
                   }
                }
            },
             new AppUser()
            {
                Name= "Steven Giesel",
                  Email= "steve@gmail.com",
                  Password = "@steve321",
                BookList = new List<Book>()
                {
                   new Book()
                   {
                        Title = "Linq Guide",
                        Author = "Giesel",
                        Description = "",
                        ISBN = "321321",
                        price = 5000,
                        AvailableCopies = 0,
                        PublishedDate = DateTime.Now.AddDays(30),
                   }
                }
            },
             new AppUser()
            {
                Name= "Erich Gamma",
                Email = "Erich@gmail.com",
                Password = "@Erich123",
                BookList = new List<Book>()
                {
                   new Book()
                   {

                        Title = "Design Patterns",
                        Author = "Gamma",
                        Description = "",
                        ISBN = "123123",
                        price = 800,
                        AvailableCopies = 0,
                        PublishedDate = DateTime.Now.AddDays(30),
                   }
                }
            },
            };



        
    }
   */
}
}
