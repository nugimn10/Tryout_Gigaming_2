using System.Security.Cryptography;
using System;
namespace payment_services.Domain.Entities
{
    public class OrdersTb
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; }
    }

    public class PaymentTb
    {
        public int id { get; set; }
        public int Order_id { get; set; }
        
        public int Transaction_id { get; set; }
        public string Payment_type { get; set; }
        public string gross_amout { get; set; }
        public DateTime Transaction_time { get; set; }
        public string Transaction_status { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; }

        public OrdersTb Orders {get; set;}
    }

    public class Order_detailTb
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; }

        public OrdersTb orders {get; set;}
        public ProductTb product { get; set; }
    }

    public class ProductTb
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; }
    }

}