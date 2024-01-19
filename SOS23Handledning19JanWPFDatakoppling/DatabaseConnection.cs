using MySql.Data.MySqlClient;
using SOS23Handledning19JanWPFDatakoppling.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SOS23Handledning19JanWPFDatakoppling
{
    public class DatabaseConnection
    {
        string server = "localhost";
        string database = "sos23procedures15jan";
        string username = "root";
        string password = "Abc123!";

        string connectionString = "";

        public DatabaseConnection()
        {
            connectionString = "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
        }

        public Dictionary<int, Student> GetStudents()
        {
            Dictionary<int, Student> students = new Dictionary<int, Student>();


            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM student;";

            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Student student = new Student((int)reader["student_id"], (string)reader["student_name"], (string)reader["email"]);
                students.Add(student.Id, student);
            }

            query = "SELECT * FROM course";

            command = new MySqlCommand(query, connection);

            reader.Close();
            reader = command.ExecuteReader();

            Dictionary<int, Course> courses = new Dictionary<int, Course>();

            while (reader.Read())
            {
                Course course = new Course((int)reader["course_id"], (string)reader["course_name"], (int)reader["points"]);
                courses.Add(course.Id, course);
            }

            query = "SELECT * FROM student_taking_course";

            command = new MySqlCommand(query, connection);

            reader.Close();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                CourseGrade grade = new CourseGrade((string)reader["grade"], courses[(int)reader["course_id"]]);
                students[(int)reader["student_id"]].Grades.Add(grade);
            }

            connection.Close();

            return students;
        }

        /// <summary>
        /// Function that updates student name.
        /// </summary>
        /// <param name="studentIdToUpdate"></param>
        /// <param name="newName"></param>
        /// <returns>True if successful, False if failure.</returns>
        public bool UpdateStudentName(int studentIdToUpdate, string newName)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "UPDATE student " +
                           "SET student_name = \"" + newName + "\" " + 
                           "WHERE student_id = " + studentIdToUpdate + ";";

            MySqlCommand command = new MySqlCommand(query, connection);

            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowsAffected > 0;
        }

        public bool DeleteStudent(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "DELETE FROM student " +
                           "WHERE student_id = " + id + ";";

            MySqlCommand command = new MySqlCommand(query, connection);

            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowsAffected > 0;
        }
    }
}
