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
            string createTableQuery = "CREATE TABLE Order (OrderID INT PRIMARY KEY, SenderAddress VARCHAR(100), Content VARCHAR(100), HasExpensiveContent BOOLEAN, Weight FLOAT, postType VARCHAR(100), Phone VARCHAR(100), Status VARCHAR(100), CustomerSSN VARCHAR(100), Date DATE, Comment VARCHAR(100))";
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
            try
            {
                string tableName = type.Name;
                PropertyInfo[] properties = type.GetProperties();

                string columns = string.Join(", ", properties.Select(p => p.Name));
                string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

                string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

                string connectionString = GlobalVariables.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        for (int i = 0; i < properties.Count(); i++)
                        {
                            command.Parameters.AddWithValue($"@{properties[i].Name}", properties[i].GetValue(instance));
                        }
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                MessageBox.Show($"{type.Name} added successfully!");
            }
            catch { MessageBox.Show($"An error while adding {type.Name}!"); }
        }
        public static void UpdateEmployeeTable(Employee employee) //----------------------------------------------------------------------------------
        {
            string updateQuery = $"UPDATE Employee SET EmployeeID = @{employee.EmployeeID}, FirstName = @{employee.FirstName}, LastName = @{employee.LastName}, Email = @{employee.Email}, UserName = @{employee.UserName}, Password = @{employee.Password}";
            try
            {
                ExecuteQuery(updateQuery);
            }
            catch { MessageBox.Show("An error occured while updating employee!"); }
        }
        public static void UpdateCustomerTable(Customer customer)
        {
            string updateQuery = $"UPDATE Customer SET SSN = @{customer.SSN}, FirstName = @{customer.FirstName}, LastName = @{customer.LastName}, Email = @{customer.Email}, UserName = @{customer.UserName}, Password = @{customer.Password}, Phone = @{customer.Phone}, Wallet = @{customer.Wallet}";
            try
            {
                ExecuteQuery(updateQuery);
            }
            catch { MessageBox.Show("An error occured while updating customer!"); }
        }
        public static void UpdateOrderTable(Order order)
        {
            string updateQuery = $"UPDATE Order SET OrderID = @{order.OrderID}, SenderAddress = @{order.SenderAddress}, RecieverAddress = @{order.RecieverAddress}, Content = @{order.Content}, HasExpensiveContent = @{order.HasExpensiveContent}, Weight = @{order.Weight}, postType = @{order.postType}, Phone = @{order.Phone}, Status = @{order.Status}, CustomerSSN = @{order.CustomerSSN}, Date = @{order.Date}, Comment = @{order.Comment}";
            try
            {
                ExecuteQuery(updateQuery);
            }
            catch { MessageBox.Show("An error occured while updating order!"); }
        }
        public static List<Employee> ReadEmployeesData()
        {
            AddEmployeeTable();
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
            AddCustomerTable();
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
            AddOrderTable();
            List<Order> Orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(GlobalVariables.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Order", connection))
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

                                    Order order = new Order(OrderID, SenderAddress, RecieverAddress, Enum.Parse<PackageContent>(Content), HasExpensiveContent, Weight, Enum.Parse<PostType>(postType), Phone, Enum.Parse<PackageStatus>(Status), CustomerSSN);
                                    Orders.Add(order);
                                    if (order.OrderID > Order.LastOrderID)
                                    {
                                        Order.LastOrderID = order.OrderID;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Orders;
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
                catch (Exception ex)
                {
                    //MessageBox.Show("Error reading data: " + ex.Message);
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
                catch (Exception ex)
                {
                    //MessageBox.Show("Error reading data: " + ex.Message);
                    return true;
                }
            }
        }
        public static object FindUSer(string username)
        {
            string connectionString = GlobalVariables.ConnectionString;
            AddEmployeeTable();
            AddCustomerTable();
            AddOrderTable();
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
                    return null;
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
