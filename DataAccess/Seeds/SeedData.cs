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
}
}
