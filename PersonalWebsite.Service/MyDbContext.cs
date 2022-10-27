using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Service.Entity;
using System;
using System.Linq;
using System.Reflection;

namespace PersonalWebsite.Service
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        #region 属性
        /// <summary>
        /// 系统更新日志
        /// </summary>
        public DbSet<SysUpdateLogEntity> SysUpdateLogs { get; set; }
        /// <summary>
        /// 定时配置
        /// </summary>
        /// <summary>
        /// 定时作业
        /// </summary>
        public DbSet<WorkEntity> Works { get; set; }
        /// <summary>
        /// 定时配置
        /// </summary>
        public DbSet<TimingEntity> Timings { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public DbSet<MailEntity> Mails { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public DbSet<StageEntity> Stages { get; set; }
        /// <summary>
        /// 留言
        /// </summary>
        public DbSet<MessageEntity> Messages { get; set; }
        /// <summary>
        /// 友情链接
        /// </summary>
        public DbSet<LinkEntity> Links { get; set; }
        /// <summary>
        /// 文章点赞
        /// </summary>
        public DbSet<ArticleRateEntity> ArticleRates { get; set; }
        /// <summary>
        /// 歌曲集合
        /// </summary>
        public DbSet<SongEntity> Songs { get; set; }
        /// <summary>
        /// 歌单集合
        /// </summary>
        public DbSet<SongMenuEntity> SongMenus { get; set; }
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
        /// <summary>
        /// 文件集合
        /// </summary>
        public DbSet<FileEntity> Files { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

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
