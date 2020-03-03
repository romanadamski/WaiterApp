using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waiter__ASP.Net.Waiter
{
    public class OrderedDish
    {
        public OrderedDish(string name, int amount)
        {
            Dish = WaiterManager.GetDish(name);
            Amount = amount;
        }
        public Dish Dish;
        int _amount;
        public int Amount { get
            {
                return _amount;
            }
            set 
            {
                if (value > 100)
                    _amount = 100;
                else if(value < 0)
                    _amount = 0;
                else
                    _amount = value;
            } }
        public double TotalPrice { get { return Amount * Dish.Price; } }
    }
}
