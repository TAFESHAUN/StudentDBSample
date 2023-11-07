using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace StudentDBSample.Data
{
    internal class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository()
        {
            _connectionString = @"Data Source=DESKTOP-TBL2MHJ;Initial Catalog=SchoolDB;Integrated Security=True;Trust Server Certificate=True";
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Age = (int)reader["Age"]
                            });
                        }
                    }
                }
            }

            return students;
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Students (Name, Age) VALUES (@Name, @Age)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Students SET Name = @Name, Age = @Age WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Students WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", studentId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
