using System.Threading.Tasks;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MedMan.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            var connection = (SqlConnection)Database.GetDbConnection();
            connection.AccessToken = (new Microsoft.Azure.Services.AppAuthentication.AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;
        }
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Administration> Administrations { get; set; }

        public async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var admin = new IdentityRole("Admin");
                await roleManager.CreateAsync(admin);

                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                };

                await userManager.CreateAsync(user, "DefaultAdminPassword1!");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (!await roleManager.RoleExistsAsync("Doctor"))
            {
                var doctor = new IdentityRole("Doctor");
                await roleManager.CreateAsync(doctor);

                IdentityUser user = new IdentityUser
                {
                    UserName = "doctor@sswmedical.com"
                };

                await userManager.CreateAsync(user, "DoctorPassword1!");
                await userManager.AddToRoleAsync(user, "Doctor");
            }

            if (!await roleManager.RoleExistsAsync("Nurse"))
            {
                var nurse = new IdentityRole("Nurse");
                await roleManager.CreateAsync(nurse);

                IdentityUser user = new IdentityUser
                {
                    UserName = "nurse@sswmedical.com"
                };

                await userManager.CreateAsync(user, "NursePassword1!");
                await userManager.AddToRoleAsync(user, "Nurse");
            }
        }
    }
}
