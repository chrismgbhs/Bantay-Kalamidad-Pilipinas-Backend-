using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Database_Manager
    {
        public static string connectionString = "Data Source=CHREGION\\SQLEXPRESS;Initial Catalog=\"Bantay Kalamidad Pilipinas\";Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"SQL Server Management Studio\";";

        // LOGIN FUNCTION
        public static bool Login(string username, string password, string role)
        {
            bool status = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query = "";

                    switch (role)
                    {
                        case "Admin":
                            query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password AND Role = 'Admin'";
                            break;
                        case "Donor":
                            query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password AND Role = 'Donor'";
                            break;
                        case "Volunteer":
                            query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password AND Role = 'Volunteer'";
                            break;
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // INITIALIZATION OF DATA

                                status = true;
                                CurrentUser.Username = username;
                                CurrentUser.Password = password;
                                CurrentUser.ID = reader["ID"].ToString();

                            }

                            else
                            {
                                Console.WriteLine("User not found.");
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            return status;
        }

        // GENERAL SHOW TABLE FUNCTIONS
        public static void ShowTable(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM {tableName}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Display column headers
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write($"{reader.GetName(i)}\t");
                                }
                                Console.WriteLine();
                                // Display rows
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write($"{reader[i]}\t");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No data found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void ShowTableWithCondition(string tableName, string condition)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM {tableName} WHERE {condition}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Display column headers
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write($"{reader.GetName(i)}\t");
                                }
                                Console.WriteLine();
                                // Display rows
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write($"{reader[i]}\t");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No data found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // ADDING PLEDGE

        public static void AddPledge(string donorID, string pledgeAmount, DateTime pledgeDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "usp_AddPledge (@donorID, ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@donorID", donorID);
                        command.Parameters.AddWithValue("@pledgeAmount", pledgeAmount);
                        command.Parameters.AddWithValue("@pledgeDate", pledgeDate);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Pledge added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add pledge.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
