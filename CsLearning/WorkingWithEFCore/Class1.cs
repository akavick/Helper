using System;

using Microsoft.EntityFrameworkCore;



namespace WorkingWithEFCore
{

    // управление соединением с базой данных
    public class NorthwindDbContext: DbContext
    {
        // свойства, сопоставляемые с таблицами в базе данных
        //public DbSet<Category> Categories { get; set; }

        //public DbSet<Product> Products { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // для Microsoft SQL Server
            optionsBuilder.UseSqlServer(
            @"Data Source=.;" +
            "Initial Catalog=Northwind;" +
            "Integrated Security=true;" +
            "MultipleActiveResultSets=true;");
        }
    }

}