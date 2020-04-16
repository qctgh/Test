using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PersonalWebsite.Service
{
    public class MyDbContext : DbContext
    {

        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<ArticleEntity> ArticleEntities { get; set; } 
        public DbSet<ChannelEntity> ChannelEntities { get; set; }

        //public MyDbContext(DbContextOptions<MyDbContext> options)
        //: base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Data Source=(local);uid=sa;pwd=123321;DataBase=PersonalWebsite");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (var type in typesToRegister)
            {

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            //Assembly asmServices = Assembly.Load(new AssemblyName("PersonalWebsite.Service"));
            //modelBuilder.ApplyConfigurationsFromAssembly(asmServices);
            //modelBuilder.Entity<ArticleEntity>().ToTable("T_Articles");
        }

    }
}
