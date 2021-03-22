using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewDemo
{
    public class TeacherService : ICrudable<Teacher>
    {
        public void Create()
        {
            throw new NotImplementedException();
        }

        public List<Teacher> GetAll()
        {
            return new List<Teacher>()
            {
                new Teacher(){ID = 1, Name = "Nikos Pappas"},
                new Teacher(){ID = 2, Name = "Nikos Pappas"},
                new Teacher(){ID = 3, Name = "Nikos Pappas"},
                new Teacher(){ID = 4, Name = "Nikos Pappas"},
                new Teacher(){ID = 5, Name = "Nikos Pappas"}
            };
        }
    }
}
