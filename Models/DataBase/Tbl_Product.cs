using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models.DataBase
{
    public class Tbl_Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        
        
    }
}