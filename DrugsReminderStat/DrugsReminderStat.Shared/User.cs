using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : INotifyPropertyChanged
    {
       // [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public Int32 phone { get; set; }
        public String icone { get; set; }
        //public List<Drug> Drugs { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

