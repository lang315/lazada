using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazada
{
    class Account
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string BirthDay { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string IP { get; set; }
        public string UsernameIP { get; set; }
        public string PasswordIP { get; set; }

        Random _random = new Random();

        public Account(string email, string pass)
        {
            Email = email;
            Password = pass;
        }

        public Account(bool onlyLikeProduct, string email, string password, string ip, string usernameIP, string passwordIP)
        {
            Email = email;
            Password = password;
            IP = ip;
            UsernameIP = usernameIP;
            PasswordIP = passwordIP;
            if (!onlyLikeProduct)
            {
                var info = new GetInfo();
                FullName = info.FullName();
                Day = info.Day;
                if (Day[0] == '0')
                    Day = Day.Remove(0, 1);
                Month = info.Month;
                if (Month[0] == '0')
                    Month = Month.Remove(0, 1);
                Year = info.Year;
                BirthDay = Year + "-" + Month + "-" + Day;
                if (_random.Next(0, 2) == 0)
                    Gender = "male";
                else
                    Gender = "female";
            }
        }
    }
}
