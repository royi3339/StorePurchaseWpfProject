using System;

namespace BE
{
    public class Order
    {
        public int id { get; set; }
        public DateTime orderDate { get; set; }
        public float orderPrice { get; set; }
        public Order(DateTime time, float price)
        {
            orderDate = time;
            orderPrice = price;
        }

        public Order(float price) 
        {           
            orderDate = DateTime.Now;
            orderPrice = price;
        }
        public Order() { }
    }
}
