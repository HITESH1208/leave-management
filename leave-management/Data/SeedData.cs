using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Data.Models;

namespace leave_management.Data
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager , RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager);
            SeedUser(userManager);
           
        }

        private static void SeedUser(UserManager<Employee> userManager)
        {
            if(userManager.FindByNameAsync("Admin").Result==null)
            {
                var user = new Employee { UserName = "Admin@gif.com", Email = "Admin@gif.com" };
                var result = userManager.CreateAsync(user,"Pa$$w0rd.1").Result;

                if(result!=null && result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        private static void SeedRole(RoleManager<IdentityRole> roleManager)
        { 
            if(!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole { Name="Admin" };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole { Name = "Employee" };
                var result = roleManager.CreateAsync(role).Result;
            }
        }

    }
}
