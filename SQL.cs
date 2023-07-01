using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp3
{
    public class SQL
    {
        public static void AddTable<T>()
        {
            string connectionString = "Data Source=;Initial Catalog=Post;Integrated Security = true;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string createTableQuery = CreateTableQuery<T>();

                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table created successfully!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating table: " + ex.Message);
                }
            }
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
        /// <summary>
        /// make the string of the sqlcommand for making a new table
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static string GetSqlDataType(Type type)
        {
            if (type == typeof(int))
                return "INT";
            else if (type == typeof(string))
                return "VARCHAR(32)";
            else if (type == typeof(decimal))
                return "DECIMAL(10,2)";
            // Add more data types as needed for your class properties

            throw new NotSupportedException($"Data type {type.Name} is not supported.");
        }
        /// <summary>
        /// add an instance to the table in sql server
        /// Exa: Employee employee = new Employee();
        /// string insertQuery = InsertIntoTable(employee);
        /// using (SqlCommand command = new SqlCommand(insertQuery, connection))  
        ///command.ExecuteNonQuery();
        ///Console.WriteLine("Record inserted successfully!");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string InsertIntoTable<T>(T instance)
        {
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties();

            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

            return insertQuery;
        }
        /// <summary>
        /// edit the data of an instance that already exists in the sql server
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string UpdateTable<T>(T instance)
        {
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties();

            string setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            string updateQuery = $"UPDATE {tableName} SET {setClause} WHERE EmployeeID = @EmployeeID";

            return updateQuery;
        }
        public static List<Employee> ReadEmployeesData(SqlConnection connection, string selectQuery)
        {
            List <Employee> employees = new List<Employee>();
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
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
                            try
                            {
                                Employee employee = new Employee(EmployeeID, FirstName, LastName, Email, Username, Password);
                                employees.Add(employee);
                            }
                            catch { }
                        }
                    }
                }
            }
            return employees;
        }
        public static List<Customer> ReadCustomersData(SqlConnection connection, string selectQuery)
        {
            List<Customer> Customers = new List<Customer>();
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Check if there are any rows returned
                    if (reader.HasRows)
                    {
                        // Read and process each row
                        while (reader.Read())
                        {
                            string SSN = reader.GetString(0);
                            string FirstName = reader.GetString(1);
                            string LastName = reader.GetString(2);
                            string Email = reader.GetString(3);
                            string Phone = reader.GetString(4);
                            double Wallet = reader.GetDouble(5);
                            string Username = reader.GetString(6);
                            string Password = reader.GetString(7);
                            try
                            {
                                Customer customer = new Customer(SSN, FirstName, LastName, Email, Phone, Wallet, Username, Password);
                                Customers.Add(customer);
                            }
                            catch { }
                        }
                    }
                }
            }
            return Customers;
        }
        public static List<Order> ReadOrdersData(SqlConnection connection, string selectQuery)
        {
            List<Order> Orders = new List<Order>();
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Check if there are any rows returned
                    if (reader.HasRows)
                    {
                        // Read and process each row
                        while (reader.Read())
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
                            try
                            {
                                Order order = new Order(OrderID, SenderAddress, RecieverAddress, Enum.Parse<PackageContent>(Content), HasExpensiveContent, Weight, Enum.Parse<PostType>(postType), Phone, Enum.Parse<PackageStatus>(Status), CustomerSSN, date);
                                Orders.Add(order);
                            }
                            catch { }
                        }
                    }
                }
            }
            return Orders;
        }
        public static bool UserExist(string Username)
        {
            string connectionString = "Data Source=;Initial Catalog=Post;Integrated Security = true;MultipleActiveResultSets=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string EmployeeselectQuery = "SELECT * FROM Employees";
                    List<Employee> employees = SQL.ReadEmployeesData(connection, EmployeeselectQuery);

                    string CustomerselectQuery = "SELECT * FROM Customers";
                    List<Customer> customers = SQL.ReadCustomersData(connection, CustomerselectQuery);
   
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
            string connectionString = "Data Source=;Initial Catalog=Post;Integrated Security = true;MultipleActiveResultSets=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string EmployeeselectQuery = "SELECT * FROM Employees";
                    List<Employee> employees = SQL.ReadEmployeesData(connection, EmployeeselectQuery);

                    string CustomerselectQuery = "SELECT * FROM Customers";
                    List<Customer> customers = SQL.ReadCustomersData(connection, CustomerselectQuery);

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
            string connectionString = "Data Source=;Initial Catalog=Post;Integrated Security = true;MultipleActiveResultSets=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string EmployeeselectQuery = "SELECT * FROM Employees";
                    List<Employee> employees = ReadEmployeesData(connection, EmployeeselectQuery);

                    string CustomerselectQuery = "SELECT * FROM Customers";
                    List<Customer> customers = ReadCustomersData(connection, CustomerselectQuery);

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
