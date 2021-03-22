using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewDemo.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(): base(@"Data Source = DESKTOP-UHUV6UP\SQLEXPRESS01; Initial Catalog = MyTestDBManyToMany;Integrated Security = True")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
