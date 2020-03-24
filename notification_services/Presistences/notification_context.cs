using System;
using Microsoft.EntityFrameworkCore;
using notification_services.Domain.Entities;

namespace notification_services.Presistences
{
    public class notification_context : DbContext
    {
        public notification_context(DbContextOptions<notification_context> options) : base(options) {}
        public DbSet<NotificationTb> Notification {get; set;}
        public DbSet<Notification_logsTb> Notification_Logs {get; set;}
        public DbSet<UserTb> User {get; set;}
        // public DbSet<Users> Users {get; set;}

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Notification_logsTb>()
            .HasOne(i => i.notification)
            .WithMany().HasForeignKey(i => i.notification_id);
            
            // model.Entity<Notification_logs>()
            // .HasOne(i => i.users)
            // .WithMany().HasForeignKey(i => i.from);
        }
        
    }
}