using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sleepr
{
    class AlarmLogic
    {
        public void setAlarm(double minutesToAlarm)
        {
            if (ScheduledActionService.Find("alarm") != null)
                ScheduledActionService.Remove("alarm");

            //  minutesToAlarm = .5;
            
            Debug.WriteLine("Minutes to alarm is " + minutesToAlarm);
            Alarm a = new Alarm("alarm");
            a.Content = "Time to wake up!";
            a.Sound = new Uri("Assets/alarm.wav", UriKind.Relative);
            a.BeginTime = DateTime.Now.AddMinutes(minutesToAlarm);
            

           
            //a.BeginTime = DateTime.Now.AddMinutes(minutesToAlarm);
            //a.ExpirationTime = DateTime.Now.AddDays(7);
            //a.RecurrenceType = RecurrenceInterval.Daily;

            //we need to schedule for the alarm to be removed 
            ScheduledActionService.Add(a);


        }

        public void clearAlarm()
        {
            ScheduledActionService.Remove("alarm");
        }

        public void removeAlarm()
        {

            if (ScheduledActionService.Find("alarm") != null)
            {
                Debug.WriteLine("Alarm was found and removed.");
                ScheduledActionService.Remove("alarm");
            }
            else
            {
                Debug.WriteLine("Alarm was not found and therefore not removed.");
            }

        }
    }
}
