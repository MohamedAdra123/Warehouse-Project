using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Purchase
    {
        public string name { set; get; }
        public int category { set; get; }
        public int price { set; get; }
        public int quantity { set; get; }
        public int total { set; get; }
    }
}
