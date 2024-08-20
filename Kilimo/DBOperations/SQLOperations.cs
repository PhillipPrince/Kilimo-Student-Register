using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Kilimo.DBOperations;
using Kilimo.Models;

namespace Kilimo.DBConnection
{
    public class SQLOperations
    {
        private readonly SqlConnectionManager _sqlConnectionManager;

        public SQLOperations(SqlConnectionManager sqlConnectionManager)
        {
            _sqlConnectionManager = sqlConnectionManager;
        }

        // Create a new class stream
        public bool CreateClassStream(ClassStream classStream)
        {
            string procedureName = "CreateClassStream";

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StreamName", classStream.StreamName);
                    command.Parameters.AddWithValue("@Description", classStream.Description);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }

        // Create a new student
        public bool CreateStudent(Student student)
        {
            string procedureName = "CreateStudent";

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentName", student.StudentName);
                    command.Parameters.AddWithValue("@StudentId", student.StudentId);
                    command.Parameters.AddWithValue("@ClassStreamId", student.ClassStreamId);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }

        // Get a list of students
        public IEnumerable<Student> GetStudents()
        {
            string procedureName = "GetStudents";

            var students = new List<Student>();

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                StudentName = reader["StudentName"].ToString(),
                                StudentId = reader["StudentId"].ToString(),
                                ClassStreamId = Convert.ToInt32(reader["ClassStreamId"])
                            };

                            students.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
            }

            return students;
        }

        // Get a single student by ID
        public Student GetStudentById(int studentId)
        {
            string procedureName = "GetStudentById";

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                StudentName = reader["StudentName"].ToString(),
                                StudentId = reader["StudentId"].ToString(),
                                ClassStreamId = Convert.ToInt32(reader["ClassStreamId"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
            }

            return null;
        }

        // Update a student's details
        public bool UpdateStudent(Student student)
        {
            string procedureName = "UpdateStudent";

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentName", student.StudentName);
                    command.Parameters.AddWithValue("@StudentId", student.StudentId);
                    command.Parameters.AddWithValue("@ClassStreamId", student.ClassStreamId);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }

        // Delete a student
        public bool DeleteStudent(int studentId)
        {
            string procedureName = "DeleteStudent";

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }

        // Get a list of class streams (if needed for dropdown)
        public IEnumerable<ClassStream> GetClassStreams()
        {
            string procedureName = "GetClassStreams";

            var classStreams = new List<ClassStream>();

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var classStream = new ClassStream
                            {
                                StreamId = Convert.ToInt32(reader["StreamId"]),
                                StreamName = reader["StreamName"].ToString(),
                                Description = reader["Description"].ToString()
                            };

                            classStreams.Add(classStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
            }

            return classStreams;
        }
        public ClassStream GetClassStreamById(int id)
        {
            string procedureName = "GetClassStreamById";

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StreamId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ClassStream
                            {
                                StreamId = Convert.ToInt32(reader["StreamId"]),
                                StreamName = reader["StreamName"].ToString(),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
            }

            return null;
        }
        // Get a list of students for a specific class stream
        public IEnumerable<Student> GetStudentsByClassStream(int classStreamId)
        {
            string procedureName = "GetStudentsByClassStream"; // Stored procedure name

            var students = new List<Student>();

            try
            {
                using (var connection = _sqlConnectionManager.GetConnection())
                using (var command = new SqlCommand(procedureName, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassStreamId", classStreamId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                StudentName = reader["StudentName"].ToString(),
                                StudentId = reader["StudentId"].ToString(),
                                ClassStreamId = Convert.ToInt32(reader["ClassStreamId"])
                            };

                            students.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
            }

            return students;
        }

    }
}
