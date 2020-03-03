using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Waiter__ASP.Net.Models;
using Waiter__ASP.Net.Waiter;

namespace Waiter__ASP.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult LoadRestaurationData(int tableAmount)
        {
            WaiterManager.LoadData(tableAmount);
            return Json(new { text = "loaded " + tableAmount });
        }
        [HttpGet]
        public JsonResult LoadListOfDishes()
        {
            var json = JsonConvert.SerializeObject(WaiterManager.Dishes);
            return Json(json);
        }

        [HttpGet]
        public JsonResult AddDishToTable(string tableName, string dishName, int amount)
        {
            var table = WaiterManager.GetTable(tableName);
            if (table == null)
                return Json(new { isTableInOrders = false, totalPrice = 0 });
            var isTableInOrders = WaiterManager.CheckTableInOrders(table);
            WaiterManager.AddDishToTable(tableName, dishName, amount);
            double totalPrice = table.TotalPrice;
            return Json(new { isTableInOrders, totalPrice });
        }
        [HttpGet]
        public JsonResult ChangeDishToTable(string tableName, string dishName, int amount)
        {
            var table = WaiterManager.GetTable(tableName);
            if (table == null)
                return Json(new { isTableInOrders = false, totalPrice = 0 });
            var isTableInOrders = WaiterManager.CheckTableInOrders(table);
            WaiterManager.ChangeDishToTable(tableName, dishName, amount);
            double totalPrice = table.TotalPrice;
            return Json(new { isTableInOrders, totalPrice });
        }
        [HttpGet]
        public JsonResult GetDishesByTable(string tableName)
        {
            var table = WaiterManager.GetTable(tableName);
            if(table == null)
                return Json(new { text = "error" });
            var json = JsonConvert.SerializeObject(table.OrderedDishes);
            return Json(new { text = "success", json });
        }
        [HttpGet]
        public JsonResult DeleteDishByTable(string tableName, string dishName)
        {
            var table = WaiterManager.GetTable(tableName);
            if (table == null)
                return Json(new { text = "error" });
            table.RemoveDish(dishName);
            var isTableInOrders = WaiterManager.CheckTableInOrders(table);
            return Json(new { text = "success", isTableInOrders, totalPrice = table.TotalPrice });
        }
        [HttpGet]
        public JsonResult DeleteOrder(string tableName)
        {
            WaiterManager.RemoveOrder(tableName);
            return Json(new { text = "success", isTableInOrders = false});
        }
        [HttpGet]
        public JsonResult GetRecipe(string tableName)
        {
            var table = WaiterManager.GetTable(tableName);
            var json = JsonConvert.SerializeObject(table);
            return Json(json);
        }
        void Test()
        {

        }
    }
}
