using System;
namespace notification_services.Domain.Entities
{
    public class NotificationTb
    {
        public int id { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime update_at { get; set; } = DateTime.Now;
    }

    public class Notification_logsTb
    {
        public int id { get; set; }
        public int notification_id { get; set; }
        public string type { get; set; }
        public int  from { get; set; }
        public int target { get; set; }
        public string email_destination { get; set; }
        public DateTime read_at { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime update_at { get; set; } = DateTime.Now;

        public NotificationTb notification {get; set;}
    }
    
    public class UserTb
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string  address { get; set; }
    }
    
}