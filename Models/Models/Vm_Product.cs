using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models.Models
{
    public class Vm_Product
    {
        public int Vm_Id { get; set; }
        public string Vm_Name { get; set; }
        public string Vm_Color { get; set; }
        public int Vm_Count { get; set; }
        public long Vm_Price { get; set; }
        public bool Vm_Status { get; set; }
        public string Vm_Image { get; set; }
        public IFormFile Vm_IMG { get; set; }
        public string Vm_CaptchaCode { get; set; }
        
        
    }
}