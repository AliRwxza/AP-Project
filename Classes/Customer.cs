using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Wallet { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Customer(string FirstName, string LastName, string SSN, string Email, string Phone, double Wallet, string UserName, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.SSN = SSN;
            this.Email = Email;
            this.Phone = Phone;
            this.Wallet = Wallet;
            this.UserName = UserName;
            this.Password = Password;
        }
    }
}
