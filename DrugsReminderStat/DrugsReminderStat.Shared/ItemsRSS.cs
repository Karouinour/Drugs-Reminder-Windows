﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DrugsReminderStat
{
    public class ItemsRSS : INotifyPropertyChanged
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
