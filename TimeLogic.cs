using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleepr
{

    class TimeLogic 
    {
        public ObservableCollection<DateTime> timesList = new ObservableCollection<DateTime>();
        public ObservableCollection<String> stringTimesList = new ObservableCollection<string>();
        DateTime dt = DateTime.Now;       
        //gjijki
        public ObservableCollection<DateTime> createTimeList(bool isGetUpAt, DateTime selectedTime, int minutesToSleep)
        {
            if (isGetUpAt == true)
            {
                selectedTime = selectedTime.Subtract(new TimeSpan(0, minutesToSleep, 0));

                DateTime currentTime = DateTime.Now;
                TimeSpan difference = (selectedTime - currentTime);

                Debug.WriteLine("Current time is " + currentTime);
                Debug.WriteLine("Selected time is " + selectedTime);
                Debug.WriteLine("Difference in hours is " + difference);
                Debug.WriteLine("Difference in minutes is " + difference.TotalMinutes);
                
                int numbersOfIterations = Math.Abs((int)(difference.TotalMinutes / 90));

                Debug.WriteLine("Number of iterations is " + numbersOfIterations);

                for (int x = 0; x < numbersOfIterations; x++)
                {
                    selectedTime = selectedTime.Subtract(new TimeSpan(1, 30, 0));
                    timesList.Add(selectedTime);
                }

                timesList = new ObservableCollection<DateTime>(timesList.Reverse());
                return timesList;
            }
            else
            {
                DateTime currentTime = DateTime.Now;
                //TimeSpan difference = (passedTime - currentTime);
                //int numbersOfIterations = Math.Abs((int)(difference.TotalMinutes / 90));
                DateTime dateCalc = selectedTime.Add(new TimeSpan(1, 30 + minutesToSleep, 0));
                timesList.Add(dateCalc);

                for (int x = 0; x < 7; x++)
                {
                    dateCalc = dateCalc.Add(new TimeSpan(1, 30, 0));
                    timesList.Add(dateCalc);
                }
 
                //timesList = new ObservableCollection<DateTime>(timesList.Reverse());
                return timesList;
            }
        }


    }
}
