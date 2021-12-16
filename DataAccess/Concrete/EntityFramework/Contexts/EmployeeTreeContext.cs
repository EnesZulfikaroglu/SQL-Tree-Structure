using Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class EmployeeTreeContext : DbContext
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=desktop-v5ur9k1\sqlexpress;
                                                            Database=EmployeeTree;
                                                            Trusted_Connection=true");
        }*/
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .Property<int>("ParentId");

            modelBuilder.Entity<Employee>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Employee>()
                .hasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }*/

        public EmployeeTreeContext()
        {

        }

        public EmployeeTreeContext(DbContextOptions<EmployeeTreeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

    }
}