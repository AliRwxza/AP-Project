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
        public static bool UserName(string username)
        {
            Regex UserNameRegex = new Regex(@"^[a-zA-Z0-9]{3,32}$");
            return UserNameRegex.IsMatch(username); //&& (!SQL.UserExist(username));
        }
        public static bool Password(string password)
        {
            Regex PasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$");
            return PasswordRegex.IsMatch(password);
        }
        public static bool Name(string name)
        {
            Regex NameRegex = new Regex(@"^[a-zA-Z]{3,32}$");
            return NameRegex.IsMatch(name);
        }

        public static bool SSN(string ssn)
        {
            Regex SsnRegex = new Regex(@"^00\d{8}$");
            return SsnRegex.IsMatch(ssn);
        }
        public static bool Email(string email)
        {
            Regex EmailRegex = new Regex(@"^[a-zA-Z0-9]{3,32}@[a-zA-Z0-9]{3,32}.[a-zA-Z]{2,3}$");
            return EmailRegex.IsMatch(email);
        }
        public static bool Phone(string phone)
        {
            Regex PhoneRegex = new Regex(@"^09\d{9}$");
            return PhoneRegex.IsMatch(phone);
        }
        public static bool EmployeeID(string id)
        {
            Regex IDRegex = new Regex(@"^\d{2}9\d{2}$");
            return IDRegex.IsMatch(id);
        }
        public static bool LUHN(string number)
        {
            // Remove any non-digit characters from the input string
            string cleanedNumber = new string(number.Where(char.IsDigit).ToArray());

            int sum = 0;
            bool alternate = false;

            // Iterate over the digits from right to left
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

            // The number is valid if the sum is divisible by 10
            return sum % 10 == 0;
        }
    }
}
