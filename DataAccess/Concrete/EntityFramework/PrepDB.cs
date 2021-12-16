using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{

    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<EmployeeTreeContext>());
            }
        }

        public static void SeedData(EmployeeTreeContext context)
        {
            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();

            if (!context.Employees.Any())
            {
                System.Console.WriteLine("Adding Default Employee Data - seeding...");


                context.Employees.AddRange(
                    new Employee() { Name = "Default", ParentId = null},
                    new Employee() { Name = "Omer", ParentId = 1},
                    new Employee() { Name = "Ayse", ParentId = 2},
                    new Employee() { Name = "Ali", ParentId = 2},
                    new Employee() { Name = "Enes", ParentId = 1},
                    new Employee() { Name = "Arda", ParentId = 5},
                    new Employee() { Name = "Yusuf", ParentId = 5},
                    new Employee() { Name = "Onur", ParentId = 7},
                    new Employee() { Name = "Elif", ParentId = 5},
                    new Employee() { Name = "Kaan", ParentId = 6}
                    );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have Employee Data - not seeding...");
            }


        }
    }
}
