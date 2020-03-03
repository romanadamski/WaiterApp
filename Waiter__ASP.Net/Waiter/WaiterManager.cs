using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waiter__ASP.Net.Waiter
{
    public static class WaiterManager
    {
        static int _tableAmount;
        public static Table[] Tables;
        public static List<Dish> Dishes = new List<Dish>();
        static List<Table> Orders = new List<Table>();
        public static void AddDishToTable(string tableName, string dish, int amount)
        {
            var table = GetTable(tableName);
            if (table != null)
                table.AddDish(dish, amount);
        }
        public static void ChangeDishToTable(string tableName, string dish, int amount)
        {
            var table = GetTable(tableName);
            if (table != null)
                table.ChangeDish(dish, amount);
        }
        public static void LoadData(int tableAmount)
        {
            LoadTables(tableAmount);
            LoadDishes();
        }
        public static void LoadTables(int tableAmount)
        {
            _tableAmount = tableAmount;
            Tables = new Table[tableAmount];
            for (int i = 0; i < Tables.Length; i++)
            {
                Tables[i] = new Table(i + 1);
            }
        }
        public static void LoadDishes()
        {
            AddDish("Schabowy", 10.50f);
            AddDish("Frytki", 6f);
            AddDish("Pierogi", 12.50f);
            AddDish("Naleśniki", 11);
            AddDish("Burger", 10);
            AddDish("Pizza", 25);
            AddDish("Spaghetti", 15);
            AddDish("Zapiekanka", 8);
            AddDish("Kawa", 7);
            AddDish("Herbata", 7);
        }
        static void AddDish(string name, double price)
        {
            Dishes.Add(new Dish(name, price));
        }
        public static Dish GetDish(string name)
        {
            var dish = Dishes.Find(x => x.Name == name);
            return dish;
        }
        public static Table GetTable(string name)
        {
            var number = 0;
            if(int.TryParse(name.Split('_')[1], out number))
                return Tables[number - 1];
            return null;
        }
        static Table GetTable(int number)
        {
            if(number < Tables.Length && number >= 0)
                return Tables[number - 1];
            return null;
        }
        public static void AddOrder(int number)
        {
            var table = GetTable(number);
            if (!CheckTableInOrders(table))
                Orders.Add(table);
        }
        public static bool CheckTableInOrders(string tableName)
        {
            var table = GetTable(tableName);
            if (Orders.Contains(table))
                return true;
            return false;
        }
        public static bool CheckTableInOrders(int number)
        {
            var table = GetTable(number);
            if (Orders.Contains(table))
                return true;
            return false;
        }
        public static bool CheckTableInOrders(Table table)
        {
            if (Orders.Contains(table))
                return true;
            return false;
        }
        public static void RemoveOrder(string tableName)
        {
            var table = GetTable(tableName);
            if (Orders.Contains(table))
            {
                Orders.Remove(table);
                table.OrderedDishes.Clear();
            }
        }
        public static bool RemoveOrder(int number)
        {
            var table = GetTable(number);
            if (Orders.Contains(table))
            {
                Orders.Remove(table);
                return true;
            }
            return false;
        }
    }
}
