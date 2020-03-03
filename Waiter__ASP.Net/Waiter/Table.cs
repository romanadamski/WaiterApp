using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waiter__ASP.Net.Waiter
{
    public class Table
    {
        public Table(int number)
        {
            Number = number;
        }
        public string Name { get { return (GetType().Name + "_" + Number).ToLower(); } }
        int Number { get; set; }
        public int OrderCount
        {
            get
            {
                return OrderedDishes.Count;
            }
        }
        public double TotalPrice { get
            {
                return CalculateTotalPrice();
            }
        }
        private double CalculateTotalPrice()
        {
            double total = 0;
            foreach (var dish in OrderedDishes)
            {
                total += dish.TotalPrice;
            }
            return total;
        }

        internal void ChangeDish(string name, int amount)
        {
            var dish = OrderedDishes.Find(x => x.Dish.Name == name);
            if (dish == null)
                OrderedDishes.Add(new OrderedDish(name, amount));
            else
                dish.Amount = amount;
            WaiterManager.AddOrder(Number);
        }

        public List<OrderedDish> OrderedDishes = new List<OrderedDish>();
        public void AddDish(string name, int amount)
        {
            var dish = OrderedDishes.Find(x => x.Dish.Name == name);
            if (dish == null)
                OrderedDishes.Add(new OrderedDish(name, amount));
            else
                dish.Amount += amount;
            WaiterManager.AddOrder(Number);
        }
        public void RemoveDish(string name)
        {
            var dish = OrderedDishes.Find(x => x.Dish.Name == name);
            if (dish != null)
            {
                OrderedDishes.Remove(dish);
                if(OrderCount == 0)
                    WaiterManager.RemoveOrder(Number);
            }
        }
    }
}
