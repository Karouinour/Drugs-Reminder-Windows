using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AlarmModel
    {
        public static int SUNDAY = 0;
        public static int MONDAY = 1;
        public static int TUESDAY = 2;
        public static int WEDNESDAY = 3;
        public static int THURSDAY = 4;
        public static int FRDIAY = 5;
        public static int SATURDAY = 6;

       // [SQLite.PrimaryKey, SQLite.AutoIncrement]
        //public int Id { get; set; }
      //  public int id_drug { get; set; }
        public int timeHour { get; set; }
        public int timeMinute { get; set; }

        public bool[] repeatingDays { get; set; }
        public bool repeatWeekly { get; set; }
        public Uri alarmTone { get; set; }
        public String name { get; set; }

        public bool isEnabled { get; set; }


        public AlarmModel()
        {
            repeatingDays = new Boolean[7];
        }

        public void setRepeatingDay(int dayOfWeek, bool value)
        {
            repeatingDays[dayOfWeek] = value;
        }
        public bool getRepeatingDay(int dayOfWeek)
        {
            return repeatingDays[dayOfWeek];
        }
    }
}
