using MamoScopeTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MamoScopeTest
{
    public class AppDbContext : DbContext
    {
        public DbSet<MotorDriver> MotorDriver { get; set; }
        public DbSet<MotorDriver> Id { get; set; }
        public DbSet<MotorDriver> SerialNumber { get; set; }
        public DbSet<MotorDriver> TestDate { get; set; }
        public DbSet<MotorDriver> Voltage { get; set; }
        public DbSet<MotorDriver> IsPassed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MamoScopeDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
