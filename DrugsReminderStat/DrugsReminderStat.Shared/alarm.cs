using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.ComponentModel;
using Windows.UI.Notifications;
using System.Runtime.Serialization;

namespace DrugsReminderStat
{
    class alarm : INotifyPropertyChanged
    {
       
        public int Id { get; set; }
        public Drug drug { get; set; }
     //   public ToastNotificationManager tosts { get; set; }
        
       

        public event PropertyChangedEventHandler PropertyChanged;
    }
    internal class NotificationData
    {
        public String ItemType { get; set; }
        public String ItemId { get; set; }
        public String DueTime { get; set; }
        public String InputString { get; set; }
        public Boolean IsTile { get; set; }

    }

    [DataContract]
    public class AlarmData
    {

        public AlarmData()
        {
            shs = new List<string>();
        }
        [DataMember]
        public int id { get; set; }
         [DataMember]
        public Drug drug { get; set; }
         [DataMember]
        public int drugid { get; set; }
        [DataMember]
        public String InputString { get; set; }
        [DataMember]
        public String ItemType { get; set; }

        [DataMember]
        public String ItemDays { get; set; }
        [DataMember]
        public String ItemId { get; set; }
        [DataMember]
        public String DueTime { get; set; }
        [DataMember]
        public Boolean IsTile { get; set; }
        [DataMember]
        public Boolean IsEnable { get; set; }
        [DataMember]
        public List<string> shs { get; set; }

    }
    [DataContract]
    internal class myNotification
    {
        [DataMember]
        public ScheduledToastNotification myShedule { get; set; }
        [DataMember]
        public int alarmId { get; set; }

    }
}
