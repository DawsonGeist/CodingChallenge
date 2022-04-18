using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class CompensationContext : DbContext
    {
        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {

        }
        /// <summary>
        /// NOTE C(1): This is the class that was used on the Microsoft Documentation to describe the relationships between Entities/ Database Tables
        /// 
        /// I would need a good day with someone who is good at this stuff to get an idea of the scope of this process and how to debug and develop further
        /// on my own.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    // Add the shadow property to the model
            //    modelBuilder.Entity<Employee>()
            //        .Property<int>("CompensationForeignKey");

            // Use the shadow property as a foreign key
            modelBuilder.Entity<Compensation>()
                .HasOne(p => p.Employee)
                .WithOne();

        }

        public DbSet<Compensation> Compensations { get; set; }
    }
}
