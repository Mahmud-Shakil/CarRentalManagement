using DotNetCore_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static DotNetCore_5.Models.AllModels.Models;

namespace DotNetCore_5.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Payment_Type> Payment_Types { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VechileImage> VechileImages { get; set; }
        public DbSet<Vehicle_Availability> Vehicle_Availabilities { get; set; }
        public DbSet<Booking_Status_Code> Booking_Status_Codes { get; set; }
        public DbSet<Availability_Code> Availability_Codes { get; set; }
    }
}
