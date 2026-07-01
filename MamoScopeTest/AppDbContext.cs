using MamoScopeTest.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MamoScopeTest
{
    public class AppDbContext : DbContext
    {
        
        public DbSet<MotorDriver> MotorDriver { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=MamoScopeDb;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlServerOptionsAction: sqlOptions =>
                {
                    
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        }
    }
}