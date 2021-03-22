using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewDemo
{
    public class StudentService : ICrudable<Student>
    {
        public void Create()
        {
            string connectionString = @"Data Source = NBD-PANBOZ-N\SQLEXPRESS; Initial Catalog = MyTestDBManyToMany;Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand query = new SqlCommand("INSERT INTO STUDENTS (Name) VALUES ('Stathis Oikonomou')", conn);
                try
                {
                    //conn.Open();
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
                    //conn.Close();
                }
            }
        }

        public List<Student> GetAll()
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

        public Student Read(int id)
        {
            string connectionString = @"Data Source = NBD-PANBOZ-N\SQLEXPRESS; Initial Catalog = MyTestDBManyToMany;Integrated Security = True";
            Student student = new Student();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand query = new SqlCommand("SELECT * FROM students WHERE ID = " + id, connection);
                    connection.Open();
                    SqlDataReader reader= query.ExecuteReader();
                    while (reader.Read())
                    {
                        student.ID = reader.GetInt32(0);
                        student.Name = reader.GetString(1);
                    }
                }
                catch(SqlException e)
                {
                    MessageBox.Show("Sql Exception: " + e.Message);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Exception: " + e.Message);
                }
                finally
                {

                }
            }
            return student;
        }
    }
}
