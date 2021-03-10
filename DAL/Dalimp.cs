using BE;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class Dalimp
    {
        public void saveProduct(Product product)
        { 
            using (var ctx = new ProductContext())
            {
                ctx.products.Add(product);
                ctx.SaveChanges();
            }
        }

        public void saveOrder(List<OrderRow> order, float price)
        {
            int idOrder;
            Order myOrder = new Order(price);
            using (var ctx = new OrderContext())
            {
                ctx.orders.Add(myOrder);
                ctx.SaveChanges();
                idOrder = ctx.orders.Max(x => x.id);
            }

            foreach (OrderRow row in order) 
            {
                row.orderId = idOrder; 
                using (var ctx = new OrderRowContext())
                {
                    ctx.RowOfOrders.Add(row);
                    ctx.SaveChanges();
                }
            }
        }

        public SortedSet<int>[] loadForApiriori()
        {
            Util util = new Util();
            List<SortedSet<int>> ordersContainer = new  List<SortedSet<int>>();
            List<Order> orderIdContainer;
            using (var ctx = new OrderContext())
            {
                orderIdContainer = ctx.orders.ToList();
            }
            foreach (Order order in orderIdContainer)
            {
                int day = util.convertDay(order.orderDate);
                int dayPeriod = util.convertDayPeriod(order.orderDate);
                SortedSet<int> dataSet;
                using (var ctx2 = new OrderRowContext())
                {
                    dataSet = new SortedSet<int>((ctx2.RowOfOrders.Where(y => y.orderId == order.id).Select(y => y.productId).Append(day).Append(dayPeriod)).ToList());
                }
                ordersContainer.Add(dataSet);
            }            
            return ordersContainer.ToArray();
        }

        public List<Product> GetProducts()
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            List<Product> pLst;
            using (var ctx = new ProductContext())
            {
                pLst = ctx.products.ToList();
            }
            return pLst;
        }

        public List<Order> getAllOrders()
        {
            List<Order>orderpLst;
            using (var ctx = new OrderContext())
            {
                orderpLst = ctx.orders.ToList();
            }
            return orderpLst;
        }

        public List<OrderRow> getAllOrderRows()
        {
            List<OrderRow> orderRowLst;
            using (var ctx = new OrderRowContext())
            {
                orderRowLst = ctx.RowOfOrders.ToList();
            }
            return orderRowLst;
        }

        public Dalimp() { }
    }
}
