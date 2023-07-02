using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Employee(string EmployeeID, string FirstName, string LastName, string Email, string UserName, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.EmployeeID = EmployeeID;
            this.Email = Email;
            this.UserName = UserName;
            this.Password = Password;
            SQL.AddTable<Employee>();
            SQL.InsertEmployeeIntoTable(this);
        }
    }
}
