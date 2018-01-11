using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopExpress
{
    public static class UserControl
    {
        private static int user_id;
        private static string lastName;
        private static string firstName;
        private static string city;
        private static string country;


        public static int logIn(string email, string pass)
        {
            using (var context = new ShopExpressEntities())
            {
                var user = context.Users
                            .Where(x => x.email == email && x.password == pass)
                            .Select(x => new { x.userID, x.firstName, x.lastName, x.city, x.country }).ToList();
                try
                {
                    user_id = user[0].userID;
                    firstName = user[0].firstName;
                    lastName = user[0].lastName;
                    country = user[0].country;
                    city = user[0].city;

                    return user[0].userID;
                }
                catch
                {
                    return 0;
                }
            };
        }

        public static void logOut()
        {

        }
        public static string userFullname()
        {
            return firstName + " " + lastName;
        }

        public static int ID()
        {
            return user_id;
        }

        public static string getCity()
        {
            return city;
        }
        public static string getCountry()
        {
            return country;
        }
    }
}
