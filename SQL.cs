using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfApp3
{
    public class SQL
    {
        static void AddTable<T>()
        {
            string connectionString = "Data Source=.;Initial Catalog=db;Integrated Security = true;MultipleActiveResultSets=true";
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
        static string CreateTableQuery<T>()
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
        static string GetSqlDataType(Type type)
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
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        static string InsertIntoTable<T>(T instance)
        {
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties();

            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

            return insertQuery;
        }
    }
}
