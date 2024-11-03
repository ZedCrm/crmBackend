using ConfApp.Mapping;
using Domain.Objects.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfApp
{
    public class MyContext : DbContext
    {
        #region DbSet 
        public DbSet<Person> Persons { get; set; }
        #endregion
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var assembly = typeof(PersonMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
