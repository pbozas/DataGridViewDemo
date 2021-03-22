using DataGridViewDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewDemo
{
    public class CourseService : ICrudable<Course>
    {
        public void Create()
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Courses.ToList();
            }
        }
    }
}
