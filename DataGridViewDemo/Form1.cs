using DataGridViewDemo.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DataGridViewDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var select = "SELECT * FROM Students";
            SqlConnection conn = new SqlConnection(@"Data Source = DESKTOP-UHUV6UP\SQLEXPRESS; Initial Catalog = MyTestDBManyToMany;Integrated Security = True");
            var dataAdapter = new SqlDataAdapter(select, conn);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;

            //dataGridView1.DataSource = GetMockStudents();
            //InsertStudentWithEF();
            //UpdateStudentWithEF();
            //StudentService studentService = new StudentService();
            //dataGridView1.DataSource = studentService.GetAll();
            //MessageBox.Show(studentService.Read(15).Name);
            //CourseService courseService = new CourseService();
            //dataGridView1.DataSource = courseService.GetAll();
            TeacherService teacherService = new TeacherService();
            dataGridView1.DataSource = teacherService.GetAll();
        }

        public void InsertStudentWithADO()
        {
            string connectionString = @"Data Source = NBD-PANBOZ-N\SQLEXPRESS; Initial Catalog = MyTestDBManyToMany;Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand query = new SqlCommand("INSERT INTO STUDENTS (Name) VALUES ('Stathis Oikonomou')",conn);
                try
                {
                    conn.Open();
                    query.ExecuteScalar();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Sql exception: " + e.Message);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception: " + e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void InsertStudentWithEF()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Students.Add(new Student() { Name = "Leonidas Panagou" });
                db.SaveChanges();
            }
        }

        // Doesn't work
        public void InsertMockStudent()
        {
            GetMockStudents().Add(new Student() { Name = "A mock student" });
        }

        public List<Student> GetMockStudents()
        {
            return  new List<Student>()
            {
                new Student()
                {
                    ID = 1,
                    Name = "Filipos Nikolaou"
                },
                new Student()
                {
                    ID = 2,
                    Name = "Vasilis Tsitsanis"
                },
                new Student()
                {
                    ID = 3,
                    Name = "Stratos Dionusiou"
                },
                new Student()
                {
                    ID = 4,
                    Name = "Panos Gavalas"
                }
            };
        }

        public List<Student> GetStudentsWithADO()
        {
            List<Student> students = new List<Student>();
            string connectionString = @"Data Source = NBD-PANBOZ-N\SQLEXPRESS; Initial Catalog = MyTestDBManyToMany;Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand query = new SqlCommand("SELECT * FROM students", conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = new Student()
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        students.Add(student);
                    }
                    reader.Close();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Sql exception: " + e.Message);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return students;
        }

        public List<Student> GetStudentsWithEF()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Students.ToList();
            }
        }

        public void UpdateStudentWithADO()
        {
            string connectionString = @"Data Source = NBD-PANBOZ-N\SQLEXPRESS; Initial Catalog = MyTestDBManyToMany;Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand query = new SqlCommand("Update Students SET Name = 'John Hanidis' WHERE ID = 16",connection);
                    connection.Open();
                    query.ExecuteScalar();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Sql Exception: " + e.Message);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception: " + e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateStudentWithEF()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Student studentToUpdate = db.Students.SingleOrDefault(s => s.ID == 16);
                studentToUpdate.Name = "Kostis Hatzithanos";
                db.SaveChanges();
            }
        }

        public void DeleteStudentWithADO()
        {
            string connectionString = @"Data Source = NBD-PANBOZ-N\SQLEXPRESS; Initial Catalog = MyTestDBManyToMany;Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand query = new SqlCommand("DELETE FROM students WHERE ID = 18", connection);
                    connection.Open();
                    query.ExecuteScalar();
                }
                catch(SqlException e)
                {
                    MessageBox.Show("Sql exeption: " + e.Message);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Exception: " + e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteStudentWithEF()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Student dontWantedStudent = db.Students.SingleOrDefault(s => s.ID == 17);
                db.Students.Remove(dontWantedStudent);
                db.SaveChanges();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = "Hey";
        }
    }
}
