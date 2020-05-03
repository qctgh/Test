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
        /// <summary>
        /// 用户集合
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }
        /// <summary>
        /// 文章集合
        /// </summary>
        public DbSet<ArticleEntity> Articles { get; set; }
        /// <summary>
        /// 频道集合
        /// </summary>
        public DbSet<ChannelEntity> Channels { get; set; }
        /// <summary>
        /// 角色集合
        /// </summary>
        public DbSet<RoleEntity> Roles { get; set; }
        /// <summary>
        /// 配置集合
        /// </summary>
        public DbSet<SettingEntity> Settings { get; set; }
        /// <summary>
        /// 后台用户集合
        /// </summary>
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        /// <summary>
        /// 字典集合
        /// </summary>
        public DbSet<KeyValueEntity> KeyValues { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public DbSet<PermissionEntity> Permissions { get; set; }
        /// <summary>
        /// 角色权限集合
        /// </summary>
        public DbSet<RolePermissionsEntity> RolePermissions { get; set; }
        /// <summary>
        /// 后台用户角色集合
        /// </summary>
        public DbSet<AdminUserRolesEntity> AdminUserRoles { get; set; }
        /// <summary>
        /// 评论集合
        /// </summary>
        public DbSet<CommentEntity> Comments { get; set; }
        /// <summary>
        /// 过滤词集合
        /// </summary>
        public DbSet<FilterWordEntity> FilterWords { get; set; }

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
            //遍历加载FluentAPI配置
            foreach (var type in typesToRegister)
            {

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

    }
}
