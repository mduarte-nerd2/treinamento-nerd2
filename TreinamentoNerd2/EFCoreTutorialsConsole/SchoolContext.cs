using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorialsConsole
{
    public class SchoolContext : DbContext
    {

        public DbSet<Student> Students { get; set; }

        public DbSet<Grade> Grades { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolMigrationDb;Trusted_Connection=True;");
        }

    }

}
