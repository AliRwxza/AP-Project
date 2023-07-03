using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Collections;

namespace WpfApp3
{
    public class SQL
    {
        public static void ExecuteQuery(string Query)
        {
            string connectionString = GlobalVariables.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public static void AddEmployeeTable()
        {
            string createTableQuery = "CREATE TABLE Employee (EmployeeID VARCHAR(100) PRIMARY KEY, FirstName VARCHAR(100), LastName VARCHAR(100), Email VARCHAR(100), UserName VARCHAR(100), Password VARCHAR(100))";
            try
            {
                ExecuteQuery(createTableQuery);
            }
            catch { }
        }
        public static void AddCustomerTable()
        {
            string createTableQuery = "CREATE TABLE Customer (SSN VARCHAR(100) PRIMARY KEY, FirstName VARCHAR(100), LastName VARCHAR(100), Email VARCHAR(100), UserName VARCHAR(100), Password VARCHAR(100), Phone VARCHAR(100), Wallet FLOAT)";
            try
            {
                ExecuteQuery(createTableQuery);
            }
            catch { }
        }
        public static void AddOrderTable()
        {
            string createTableQuery = "CREATE TABLE [Order] (OrderID INT, SenderAddress VARCHAR(100), RecieverAddress VARCHAR(100), Content VARCHAR(100), HasExpensiveContent BIT, Weight FLOAT, postType VARCHAR(100), Phone VARCHAR(100), Status VARCHAR(100), CustomerSSN VARCHAR(100), Date DATE, Comment VARCHAR(100))";
            try
            {
                ExecuteQuery(createTableQuery);
            }
            catch { }
        }
        public static void AddTable<T>()
        {
            string createTableQuery = CreateTableQuery<T>();
            try
            {
                ExecuteQuery(createTableQuery);
            }
            catch { }
        }
        public static string CreateTableQuery<T>()
        {
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties();

            string createTableQuery = $"CREATE TABLE {tableName} (";

            foreach (PropertyInfo property in properties)
            {
                string columnName = property.Name;
                string dataType = GetSqlDataType(property.PropertyType);

                createTableQuery += $"{columnName} {dataType}, ";
            }

            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

            return createTableQuery;
        }
        public static string GetSqlDataType(Type type)
        {
            if (type == typeof(int))
                return "INT";
            else if (type == typeof(string))
                return "VARCHAR(100)";
            else if (type == typeof(double))
                return "DOUBLE PRECISION";
            // Add more data types as needed for your class properties

            throw new NotSupportedException($"Data type {type.Name} is not supported.");
        }
        public static void InsertIntoTable<T>(T instance)
        {
            Type type = typeof(T);
            //try
            //{
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties();

            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
            string insertQuery = $"INSERT INTO [{tableName}] ({columns}) VALUES ({values})";
            //if (instance is Order)
            //{
            //    insertQuery = $"INSERT INTO [{tableName}] ({columns}) VALUES ({values})";
            //}
            //else
            //{
            //    insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            //}
            
            string connectionString = GlobalVariables.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    //command.Parameters.AddWithValue($"@LastOrderID", Order.LastOrderID);
                    for (int i = 0; i < properties.Count(); i++)
                    {
                        command.Parameters.AddWithValue($"@{properties[i].Name}", properties[i].GetValue(instance));
                    }
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            MessageBox.Show($"{type.Name} added successfully!");
            //}
            //catch { MessageBox.Show($"An error while adding {type.Name}!"); }
        }
        public static void UpdateTable<T>(T instance)
        {
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties();
            string columns = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            string UpdateQuery = $"UPDATE [{tableName}] SET {columns}";
            string connectionString = GlobalVariables.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateQuery, connection))
                {
                    //command.Parameters.AddWithValue($"@LastOrderID", Order.LastOrderID);
                    for (int i = 0; i < properties.Count(); i++)
                    {
                        command.Parameters.AddWithValue($"@{properties[i].Name}", properties[i].GetValue(instance));
                    }
                }
                connection.Close();
            }
            //MessageBox.Show($"{type.Name} updated successfully!");

        }
        public static List<Employee> ReadEmployeesData()
        {
            List <Employee> employees = new List<Employee>();
            //try
            //{
                using (SqlConnection connection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Employee", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read and process each row
                                while (reader.Read())
                                {

                                    string EmployeeID = reader.GetString(0);
                                    string FirstName = reader.GetString(1);
                                    string LastName = reader.GetString(2);
                                    string Email = reader.GetString(3);
                                    string Username = reader.GetString(4);
                                    string Password = reader.GetString(5);

                                    Employee employee = new Employee(EmployeeID, FirstName, LastName, Email, Username, Password);
                                    employees.Add(employee);


                                }
                            }
                        }
                    }
                    connection.Close();
                }
            //}
            //catch
            //{
            //    MessageBox.Show("Error in reading EmployeesData");
            //}
            return employees;
        }
        public static List<Customer> ReadCustomersData()
        {
            List<Customer> Customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(GlobalVariables.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are any rows returned
                        if (reader.HasRows)
                        {
                            // Read and process each row
                            while (reader.Read())
                            {
                                try
                                {
                                    string SSN = reader.GetString(0);
                                    string FirstName = reader.GetString(1);
                                    string LastName = reader.GetString(2);
                                    string Email = reader.GetString(3);
                                    string Username = reader.GetString(4);
                                    string Password = reader.GetString(5);
                                    string Phone = reader.GetString(6);
                                    double Wallet = reader.GetDouble(7);

                                    Customer customer = new Customer(SSN, FirstName, LastName, Email, Phone, Wallet, Username, Password);
                                    Customers.Add(customer);
                                }
                                catch
                                {
                                    MessageBox.Show("Error reading customer data!");
                                }
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Customers;
        }
        public static List<Order> ReadOrdersData()
        {
            List<Order> Orders = new List<Order>();
            try
            {
                using (SqlConnection connection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM [Order]", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read and process each row
                                while (reader.Read())
                                {
                                    //try
                                    //{
                                    int OrderID = reader.GetInt32(0);
                                    string SenderAddress = reader.GetString(1);
                                    string RecieverAddress = reader.GetString(2);
                                    string Content = reader.GetString(3);
                                    bool HasExpensiveContent = reader.GetBoolean(4);
                                    double Weight = reader.GetDouble(5);
                                    string postType = reader.GetString(6);
                                    string Phone = reader.GetString(7);
                                    string Status = reader.GetString(8);
                                    string CustomerSSN = reader.GetString(9);
                                    DateTime date = reader.GetDateTime(10);
                                    string Comment = reader.GetString(11);
                                    Order order = new Order(OrderID, SenderAddress, RecieverAddress, Enum.Parse<PackageContent>(Content), HasExpensiveContent, Weight, Enum.Parse<PostType>(postType), Phone, Enum.Parse<PackageStatus>(Status), CustomerSSN, date);
                                    order.Comment = Comment;
                                    Orders.Add(order);
                                    if (order.OrderID > Order.LastOrderID)
                                    {
                                        Order.LastOrderID = order.OrderID;
                                    }
                                    //}
                                    //catch { }
                                }
                            }
                        }
                    }
                }
                return Orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Orders;
            }
            
        }
        public static bool UserExist(string Username)
        {
            string connectionString = GlobalVariables.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    List<Employee> employees = SQL.ReadEmployeesData();
                    List<Customer> customers = SQL.ReadCustomersData();
   
                    foreach (var username in employees.Select(x => x.UserName))
                    {
                        if (Username == username)
                        {
                            return true;
                        }
                    }
                    foreach (var username in customers.Select(x => x.UserName))
                    {
                        if (Username == username)
                        {
                            return true;
                        }
                    }
                    return false;

                    
                }
                catch {
                    return true;
                }
            }
        }
        public static bool PasswordExist(string Password)
        {
            string connectionString = GlobalVariables.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<Employee> employees = SQL.ReadEmployeesData();

                    List<Customer> customers = SQL.ReadCustomersData();

                    foreach (var password in employees.Select(x => x.Password))
                    {
                        if (Password == password)
                        {
                            return true;
                        }
                    }
                    foreach (var password in customers.Select(x => x.Password))
                    {
                        if (Password == password)
                        {
                            return true;
                        }
                    }
                    return false;


                }
                catch
                {
                    return true;
                }
            }
        }
        public static object? FindUSer(string username)
        {
            if (!Validation.UserName(username))
            {
                return null;
            }
            string connectionString = GlobalVariables.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    List<Employee> employees = ReadEmployeesData();
                    List<Customer> customers = ReadCustomersData();

                    for (int i = 0; i < employees.Count(); i++)
                    {
                        if (employees[i].UserName == username)
                        {
                            return employees[i];
                        }
                    }
                    for (int i = 0; i < customers.Count(); i++)
                    {
                        if (customers[i].UserName == username)
                        {
                            return customers[i];
                        }
                    }
                    return new object();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading data: " + ex.Message);
                    return null;
                }
            }
        }
    }
}
