using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Drug : INotifyPropertyChanged
    {
       // [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public User user { get; set; }
        public int iduser { get; set; }
        public String name { get; set; }
       // public DateTime start_date { get; set; }
        public String type_medoc { get; set; }
        public String instruction { get; set; }
       // public String durer { get; set; }
       // public int nombre_durer { get; set; }
        //public List<AlarmModel> Alarms { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
