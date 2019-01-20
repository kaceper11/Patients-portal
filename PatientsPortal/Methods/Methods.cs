using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel;
using PatientsPortalModel.Entities;

namespace PatientsPortal.Methods
{
    public class Methods
    {
        public static string ConvertDateTo24HFormat(DateTime? date)
        {
            if (date.HasValue)
            {
                string dateString = date.Value.ToString("g", CultureInfo.GetCultureInfo("pl-PL"));
                return dateString;
            }

            return null;
        }

        public static string ConvertDateToDateWithoutTime(DateTime? date)
        {
            if (date.HasValue)
            {
                string dateString = date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pl-PL"));           
                return dateString;
            }

            return null;
        }

        public static int GetUserIdByUserName(string username)
        {
            using (PatientsPortalContext dbContext = new PatientsPortalContext())
            {
                int userId = (from u in dbContext.Users
                    where u.UserName == username
                          select u.UserId).FirstOrDefault();

                return userId;
            }
        }

        public static string GetUserNameByLogin(string login)
        {
            using (PatientsPortalContext dbContext = new PatientsPortalContext())
            {
                string username = (from u in dbContext.Users
                    where string.Compare(login, u.UserName) == 0
                    select u.UserName).FirstOrDefault();

                return !string.IsNullOrEmpty(username) ? username : string.Empty;
            }
        }

        public static bool CheckOldPassword(int userId, string oldPassword)
        {
            using (PatientsPortalContext dbContext = new PatientsPortalContext())
            {
                string password = (from u in dbContext.Users
                    where u.UserId == userId
                    select u.Password).FirstOrDefault();
                if (oldPassword == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public static bool GetUserByEmail(string email, int userId)
        {
            using (PatientsPortalContext dbContext = new PatientsPortalContext())
            {
                string mail = (from u in dbContext.Users
                    where u.UserId == userId
                    select u.Email).FirstOrDefault();

                if (mail == email)
                {
                    return true;
                }

                return false;
            }
        }

        public static bool GetUserByLogin(string login, int userId)
        {
            using (PatientsPortalContext dbContext = new PatientsPortalContext())
            {
                string username = (from u in dbContext.Users
                    where u.UserId == userId
                    select u.UserName).FirstOrDefault();

                if (username ==  login)
                {
                    return true;
                }

                return false;
            }
        }

        public static string ConvertIntToTimeString(int time)
        {            
            TimeSpan timespan = TimeSpan.FromMinutes(time);
            string stringTimeSpan = string.Format("{0:00}:{1:00}", timespan.Hours, timespan.Minutes);
            return stringTimeSpan;
        }

        public static int ConvertTimeSpanToInt(TimeSpan? time)
        {
            if (time.HasValue)
            {
                int timedb = (int)time.Value.TotalMinutes;
                return timedb;
            }

            return 0;
        }

        public static TimeSpan? ConvertIntToTimeSpan(int? totalMinutes)
        {
            if (totalMinutes.HasValue)
            {
                TimeSpan timeSpan = TimeSpan.FromMinutes((double)totalMinutes);
                return timeSpan;
            }

            return TimeSpan.Zero;
        }

        public static DateTime ConvertStringToDateTime(string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss.fffffff",
                System.Globalization.CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}
