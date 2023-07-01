using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WpfApp3.Functions
{
    public class Validation
    {
        public static bool UserName(string username)
        {
            Regex UserNameRegex = new Regex(@"^user[0-9]{4}$");
            if (SQL.UserExist(username))
            {
                return false;
            }
            if (!UserNameRegex.IsMatch(username)) 
            {
                return false;
            }
            return true;
        }
        public static bool Password(string password)
        {
            Regex PasswordRegex  = new Regex(@"^[0-9]{8}$");
            if (!PasswordRegex.IsMatch(password))
            {
                return false;
            }
            return true;
        }
        public static bool Name(string name)
        {
            Regex NameRegex = new Regex(@"^[a-zA-Z]{3,32}$");
            if (!NameRegex.IsMatch(name))
            {
                return false;
            }
            return true;
        }
        public static bool Name(string name)
        {

        }

    }
}
