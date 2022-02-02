using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProject.Models;
using Newtonsoft.Json;

namespace FinalProject.Controllers
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
            var Data = new Warehouse().QueryReader("SELECT * FROM WAREHOUSE,C_COUNTRIES_TB,C_CITIES_TB,C_STATES_TB WHERE WAREHOUSE.COUNTRY_ID=C_COUNTRIES_TB.COUNTRY_ID AND WAREHOUSE.CITY_ID=C_CITIES_TB.CITY_ID AND WAREHOUSE.STATE_ID=C_STATES_TB.STATE_ID");
            var Data2 = new Warehouse().QueryReader("SELECT * FROM C_COUNTRIES_TB");
            ViewBag.adra = Data.DefaultView;
            ViewBag.Country = Data2.DefaultView;
            return View();
        }
        
        public IActionResult Privacy(string ID)
        {
            var Data = new Warehouse().QueryReader("SELECT  *  FROM CATEGORY_TB WHERE CATEGORY_TB.WAREHOUSE_ID= "+ID);
            ViewBag.Cat = Data.DefaultView;
            return View();
        }
        [HttpPost]
        public string add(string ID, string Name,string Type,string Address, string City, string State,string Country,string Telephone,string Virtual)
        {
            //Debug.WriteLine(user);
           // Warehouse1 fr = Newtonsoft.Json.JsonConvert.DeserializeObject<Warehouse1>(person);

            var added_student = new Warehouse().Add(Name, Country,State,City,Type, Address, Telephone, Virtual);
            return Name;
        }
        public string Update(string ID,string Name, string Type, string Address, string City, string State, string Country, string Telephone, string Virtual)
        {
            //Debug.WriteLine(user);
            // Warehouse1 fr = Newtonsoft.Json.JsonConvert.DeserializeObject<Warehouse1>(person);

            var added_student = new Warehouse().Update(ID,Name,Type, Address,Country,City,State, Telephone, Virtual);
            return Name;
        }
        public string DeleteUser(string USER_ID)
        {
            var count = new Warehouse().QueryReader("SELECT COUNT(*) AS COUNT FROM CATEGORY_TB WHERE CATEGORY_TB.WAREHOUSE_ID=" + USER_ID);
            
            int x = Int32.Parse(count.Rows[0]["COUNT"].ToString());
            Console.WriteLine(x);
            if (x==0)
            {
                var DELETED_USER_ID = new Warehouse().DELETE_USER(USER_ID);
                return USER_ID;
            }
            
            return "-1";
        }
        public JsonResult GetStates(int id)
        {
            var data = new Warehouse().QueryReader("SELECT * FROM C_STATES_TB WHERE COUNTRY_ID=" + id + "");

            return Json(data);
        }
        public string DeletePurchase(string ID)
        {
            var DELETED_USER_ID = new Warehouse().DELETE_PURCHASE(ID);
            return ID;
        }
        public JsonResult GetCities(int id)
        {
            var data = new Warehouse().QueryReader("SELECT * FROM C_CITIES_TB WHERE STATE_ID=" + id + "");

            return Json(data);
        }
        public JsonResult GetWarehouses()
        {
            var Data = new Warehouse().QueryReader("SELECT * FROM WAREHOUSE,C_COUNTRIES_TB,C_CITIES_TB,C_STATES_TB WHERE WAREHOUSE.COUNTRY_ID=C_COUNTRIES_TB.COUNTRY_ID AND WAREHOUSE.CITY_ID=C_CITIES_TB.CITY_ID AND WAREHOUSE.STATE_ID=C_STATES_TB.STATE_ID");


            return Json(Data);
        }
        [HttpPost]
        public JsonResult GetCategory(string ID)
        {
            var Data = new Warehouse().QueryReader("SELECT * FROM CATEGORY_TB WHERE CATEGORY_TB.WAREHOUSE_ID= " + ID);
            ViewBag.Item = Data.DefaultView;

            return Json(Data);
        }
        [HttpPost]
        public string addItem(string name, int type, int id, int price)
        {
            //var idd = new Warehouse().QueryReader("SELECT WAREHOUSE_ID AS ID FROM CATEGORY_TB WHERE CATEGORY_TB.CATEGORY_NAME= " + ware_name);
            //int id = Int32.Parse(idd.Rows[0]["ID"].ToString());
            var data = new Warehouse().Add_Item(name,type,id,price);
            return name;
        }
        [HttpPost]
        public JsonResult GetPurchases([FromBody]List<Purchase> purchase)
        {

            foreach (Purchase p in purchase)
            {
                Console.WriteLine(p.name);
                var count = new Warehouse().QueryReader("SELECT COUNT(*) AS COUNT FROM PURCHASE WHERE PURCHASE.ITEM_NAME= " + p.name);
                int x = Int32.Parse(count.Rows[0]["COUNT"].ToString());
                if (x == 0)
                {
                    var Data = new Warehouse().Add_Purchase(p.category, p.price, p.total, p.name, p.quantity);

                   
                }
            }
            return Json(purchase);
        }
        public JsonResult GetPurchase(string ID)
        {
            var Data = new Warehouse().QueryReader("SELECT * FROM PURCHASE WHERE PURCHASE.CATEGORY_ID =" + ID);
            return Json(Data);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
