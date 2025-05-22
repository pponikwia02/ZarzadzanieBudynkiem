using IO.MainApp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IO.DataBase
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring="Data Source=localhost;Initial Catalog=Administratorzy;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;";
            optionsBuilder.UseSqlServer(connectionstring);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<AppUser>Users { get; set; }
        public DbSet<Sala>Sale { get; set; }
    }
}
