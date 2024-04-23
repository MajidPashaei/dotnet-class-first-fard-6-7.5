using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project.Models.DataBase;
namespace project.Models.Context
{
    public class Application : DbContext
    {
        public Application(DbContextOptions<Application> options) : base(options) { }
        public DbSet<Tbl_Student> tbl_student { get; set; }
        public DbSet<Tbl_Teacher> tbl_teacher { get; set; }
        public DbSet<Tbl_Product> tbl_Products { get; set; }
        public DbSet<Tbl_User> tbl_users { get; set; }
    }
}

