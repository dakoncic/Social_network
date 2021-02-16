using Microsoft.AspNetCore.Identity;
using ModelsLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunAndGames.Areas.Identity.Services
{
    public static class RoleInitializer
    {

        public static async Task Initialize(RoleManager<ApplicationRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Club"))
            {
                var role = new ApplicationRole("Club");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Guest"))
            {
                var role = new ApplicationRole("Guest");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new ApplicationRole("Admin");
                await roleManager.CreateAsync(role);
            }
        }
    }
}
