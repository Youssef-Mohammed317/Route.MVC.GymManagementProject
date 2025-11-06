using GymManagement.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.DataSeed
{
    public static class IdentityDbContextDataSeeding
    {
        public static async Task SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {

            try
            {
                var HasUsers = userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();

                if (HasRoles && HasUsers)
                {
                    return;
                }
                if (!HasRoles)
                {
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                        new IdentityRole { Name = "SuperAdmin", NormalizedName = "SUPERADMIN" }
                    };
                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role.Name!);
                        if (!roleExists)
                        {
                            var result = await roleManager.CreateAsync(role);
                            if (!result.Succeeded)
                            {
                                throw new Exception($"Failed to create role: {role.Name}");
                            }
                        }
                    }
                }
                if (!HasUsers)
                {
                    var mainAdmin = new ApplicationUser()
                    {
                        FirstName = "Youssef",
                        LastName = "Mohamed",
                        UserName = "ym_317",
                        NormalizedUserName = "ym_317",
                        Email = "youssef@gmail.com",
                        PhoneNumber = "01000000000",
                    };
                    var result = await userManager.CreateAsync(mainAdmin, "Youssef@123");
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create main admin user.");
                    }
                    result = await userManager.AddToRoleAsync(mainAdmin, "SuperAdmin");
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to assign SuperAdmin role to main admin user.");
                    }
                    var admin = new ApplicationUser()
                    {
                        FirstName = "Omar",
                        LastName = "Mohamed",
                        UserName = "om_217",
                        NormalizedUserName = "OM_217",
                        Email = "omar@gmail.com",
                        PhoneNumber = "01000000001",
                    };
                    result = await userManager.CreateAsync(admin, "Omar@123");
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create admin user.");
                    }
                    result = await userManager.AddToRoleAsync(admin, "Admin");
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to assign SuperAdmin role to main admin user.");
                    }


                }

            }
            catch (Exception ex)
            {
                throw new Exception($"seed Faild {ex.Message}");
            }
        }
    }
}
