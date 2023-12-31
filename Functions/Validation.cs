using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WpfApp3
{
    public class Validation
    {
        public static bool UserName (string username)
        {
            Regex UserNameRegex = new Regex(@"^[a-zA-Z0-9]{3,32}$");
            return UserNameRegex.IsMatch(username);
        }
        public static bool Password (string password)
        {
            Regex PasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$");
            return PasswordRegex.IsMatch(password);
        }
        public static bool Name (string name)
        {
            Regex NameRegex = new Regex(@"^[a-zA-Z]{3,32}$");
            return NameRegex.IsMatch(name);
        }

        public static bool SSN (string ssn)
        {
            Regex SsnRegex = new Regex(@"^00\d{8}$");
            return SsnRegex.IsMatch(ssn);
        }
        public static bool Email (string email)
        {
            Regex EmailRegex = new Regex(@"^[a-zA-Z0-9]{3,32}@[a-zA-Z0-9]{3,32}\.[a-zA-Z]{2,3}$");
            return EmailRegex.IsMatch(email);
        }
        public static bool Phone (string phone)
        {
            Regex PhoneRegex = new Regex(@"^09\d{9}$");
            return PhoneRegex.IsMatch(phone);
        }
        public static bool EmployeeID(string id)
        {
            Regex IDRegex = new Regex(@"^\d{2}9\d{2}$");
            return IDRegex.IsMatch(id);
        }
        public static bool CVV2(string cvv2)
        {
            Regex CVV2Regex = new Regex(@"^\d{3,4}$");
            return CVV2Regex.IsMatch(cvv2);
        }
        public static bool LUHN (string number)
        {
            string cleanedNumber = new string(number.Where(char.IsDigit).ToArray());
            int sum = 0;
            bool alternate = false;
            for (int i = cleanedNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cleanedNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit = (digit % 10) + 1;
                    }
                }
                sum += digit;
                alternate = !alternate;
            }
            return sum % 10 == 0;
        }
        public static bool UniqueUsername (string Username)
        {
            if (SQL.ReadCustomersData().Where(x => x.UserName == Username).Any() || SQL.ReadEmployeesData().Where(x => x.UserName == Username).Any())
            {
                return false;
            }
            return true;
        }
        public static bool UniqueEmail(string Email)
        {
            if (SQL.ReadCustomersData().Where(x => x.Email == Email).Any() || SQL.ReadEmployeesData().Where(x => x.Email == Email).Any())
            {
                return false;
            }
            return true;
        }
        public static bool UniquePhone(string Phone)
        {
            if (SQL.ReadCustomersData().Where(x => x.Phone == Phone).Any())
            {
                return false;
            }
            return true;
        }
    }
}