using DrugsReminderStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class SharedInformattion
    {
        public static User user { get; set; }
        public static Drug drug { get; set; }

        public static List<User> userss { get; set; }
        public static ItemsRSS item { get; set; }


        public static bool[] tab = { false, false, false, false, false, false, false };
        public static string[] days1 = { "Sun", "Mon", "Tues", "Wed", "Thur", "Fri", "Sat" };
        public static string WeekDays = "";
        public static string sharedSound = "";
        public static System.DayOfWeek[] days = { System.DayOfWeek.Sunday, System.DayOfWeek.Monday, System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday, System.DayOfWeek.Saturday };
        
    }
}
