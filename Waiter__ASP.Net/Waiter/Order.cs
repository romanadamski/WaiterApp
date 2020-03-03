using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waiter__ASP.Net.Waiter
{
    public class Order
    {
        public Order(Table table, double totalPrice)
        {
            Table = table;
            TotalPrice = totalPrice;
        }
        public Table Table { get; set; }
        public double TotalPrice { get 
            { if (Table != null)
                    return Table.TotalPrice;
                return 0;
            } set { } }
    }
}
