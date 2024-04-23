using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models.Models
{
    public class Vm_User
    {
        public int Vm_Id { get; set; }
        public string Vm_Name { get; set; }
        public string Vm_Phone { get; set; }
        public string Vm_Email { get; set; }
        public string Vm_Password { get; set; } 
        public string Vm_RePassword { get; set; } 
        
    }
}