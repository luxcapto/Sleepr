using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using Telerik.Windows.Controls;
using Windows.Foundation.Collections;
using Windows.Storage;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Threading;

namespace Sleepr
{
    public partial class MainPanorama : PhoneApplicationPage
    {

        ObservableCollection<DateTime> listOfTimes = new ObservableCollection<DateTime>();
        ObservableCollection<String> stringTimesList = new ObservableCollection<string>();
        TimeLogic timeLogic = new TimeLogic();
        AlarmLogic alarmLogic = new AlarmLogic();
        bool isGetUpAt;


        public MainPanorama()
        {
            InitializeComponent();
            setupDefaults();  
        }

        private void Panorama_Loaded(object sender, RoutedEventArgs e)
        {
            if (ScheduledActionService.Find("alarm") != null)
            {
                if (ScheduledActionService.Find("alarm").BeginTime < DateTime.Now)
                {
                    alarmLogic.clearAlarm();
                }
            }
            
        }

        private void setupDefaults()
        {
            
            //  I WOULD LIKE TO GET UP AT NEEDS TO SET ALARM TO TIME PICKER + MAYBE ADD REMINDER TO GO TO SLEEP
            List<string> options = new List<string>() { "I would like to get up at...", "I would like to go to sleep at..." };
            this.timeOptionPicker.ItemsSource = options;

            List<int> minutes = new List<int>() { 15, 30, 45, 60 };
            this.minutePicker.ItemsSource = minutes;
        }

        private void createAlarmList()
        {            
            //time object exists up top
            TimeLogic timeObject = new TimeLogic();

            if (timeOptionPicker.SelectedIndex == 0)
            {
                isGetUpAt = true;
            }
            else
            {
                isGetUpAt = false;
            }

            int minutesToSleep = Convert.ToInt32(minutePicker.SelectedValue.ToString());

            if (isGetUpAt==true)
            {

                helperText.Text = "Swipe over to see when you should go to sleep. Tap on any list item to set an alarm at " + timePicker.Value.Value.ToShortTimeString() + ".";
                stringTimesList.Clear();
                listOfTimes = timeObject.createTimeList(isGetUpAt, timePicker.Value.Value, minutesToSleep);

                foreach (DateTime item in listOfTimes)
                {
                    stringTimesList.Add(item.ToShortTimeString());
                }

                this.radDataBoundListBox.ItemsSource = stringTimesList;

            }
            else
            {

                helperText.Text = "Swipe over to set a wake up alarm.  Tap on any time suggestion to set an alarm.";
                
                stringTimesList.Clear();
                listOfTimes = timeObject.createTimeList(isGetUpAt, timePicker.Value.Value, minutesToSleep);

                foreach (DateTime item in listOfTimes)
                {
                    stringTimesList.Add(item.ToShortTimeString());
                }


                this.radDataBoundListBox.ItemsSource = stringTimesList;
            }

            if (stringTimesList.Count == 0)
            {
                alarmHelperText.Text = "Try a different time.";
            }
            else
            {
                alarmHelperText.Text = "Tap one of the times below:";
            }
        }

        private void createFinalList()
        {            
            if (timePicker.Value == null)
            {
                return;
            }
            else
            {
                createAlarmList();
            }
        }
       
        private void radDataBoundListBox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
          
            //If a tap does not register to a list item (empty list), bail out. Otherwise, continue calling alarm setting magic.
            if(radDataBoundListBox.SelectedValue == null)
            {
                //Debug.WriteLine("Selected value is " + radDataBoundListBox.SelectedValue);
                return;
            }
            else 
            {
                string returnedValue = radDataBoundListBox.SelectedValue.ToString();
                
                int index = radDataBoundListBox.GetDataSourceItemForDataItem(radDataBoundListBox.SelectedValue).Index;
                DateTime notification = listOfTimes.ElementAt(index);
                DateTime now = DateTime.Now;

                if (isGetUpAt == true)
                {
                    RadMessageBox.Show("Set " + timePicker.Value.Value.ToShortTimeString() + " alarm?", closedHandler: (args) =>
                    {

                        DialogResult result = args.Result;
                        //DateTime dt = Convert.ToDateTime(returnedValue);
                        //int secondsToAlarm = Convert.ToInt32(dt.Second.ToString());

                        DateTime selectedTime = timePicker.Value.Value;
                        //DateTime notification = DateTime.Parse(returnedValue);

                        //Debug.WriteLine(selectedTime);
                        //Debug.WriteLine(notification);

                        TimeSpan timeToAlarm;
                        //was now - sT
                        timeToAlarm = (selectedTime - now);
                        //double minutesToAlarm = Math.Abs(timeToAlarm.TotalMinutes);

                        double minutesToAlarm = timeToAlarm.TotalMinutes;

                        //Debug.WriteLine(timeToAlarm);
                        //Debug.WriteLine(minutesToAlarm);

                        if (result == 0)
                        {
                            alarmLogic.setAlarm(minutesToAlarm);
                            alarmStatus.Text = "Alarm set to " + timePicker.Value.Value.ToShortTimeString() + ".";

                            //Debug.WriteLine("Alarm was set.");
                        }
                    });
                    //deselects list selection after tap
                    radDataBoundListBox.SelectedItem = null;
                }

                else
                {
                    RadMessageBox.Show("Set " + returnedValue + " alarm?", closedHandler: (args) =>
                    {

                        DialogResult result = args.Result;
                        //DateTime dt = Convert.ToDateTime(returnedValue);
                        //int secondsToAlarm = Convert.ToInt32(dt.Second.ToString());

                        DateTime selectedTime = timePicker.Value.Value;
                        DateTime tappedTime = DateTime.Parse(returnedValue);

                        Debug.WriteLine(selectedTime);
                        Debug.WriteLine(notification);

                        TimeSpan timeToAlarm;
                        timeToAlarm = (notification - now);
                        double minutesToAlarm = timeToAlarm.TotalMinutes;

                        Debug.WriteLine(timeToAlarm);
                        Debug.WriteLine(minutesToAlarm);

                        if (result == 0)
                        {
                            timeToAlarm = (notification - now);
                            minutesToAlarm = Math.Abs(timeToAlarm.TotalMinutes);
                            alarmLogic.setAlarm(minutesToAlarm);
                            alarmStatus.Text = "Alarm set to " + returnedValue + ".";

                        }
                    });
                    radDataBoundListBox.SelectedItem = null;

                }


            }
        }            
            

        private void timeOptionPicker_StateChanged(object sender, ListPickerStateChangedEventArgs e)
        {
            createFinalList();
            if(isGetUpAt==false)
            {
                timePicker.Value = DateTime.Now;
            }

        }

        private void timePicker_ValueChanged(object sender, ValueChangedEventArgs<object> args2)
        {
            DateTime now = DateTime.Now;
            DateTime selected = timePicker.Value.Value;
            TimeSpan span = selected - now;
            int time = (int)span.TotalMinutes;
            Debug.WriteLine("The time is " + time);
            now = now.AddMinutes(-5);

            if (selected < now )
            {
                selected = selected.AddDays(1);
                timePicker.Value = selected;
            }

            //if (time < 0)
            //{
            //    RadMessageBox.Show("Looks like you're trying to go back in time! Select a future time.", closedHandler: (args) =>
            //    {
            //        return;
            //    });
            //}

            //else
            //{
                createFinalList();
            //}
            
            Debug.WriteLine("Current time is " + now);
            Debug.WriteLine("Selected time is " + selected);
            //Debug.WriteLine("Diference is " + span); 
            
        }

        private void minutePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            createFinalList();
        }

        private void timePicker_Loaded(object sender, RoutedEventArgs e)
        {
            timePicker.Value = DateTime.Now;
            if (ScheduledActionService.Find("alarm") != null)
            {
                alarmStatus.Text = "Alarm set to " + ScheduledActionService.Find("alarm").BeginTime.ToShortTimeString() +".";
            }
            else
            {
                alarmStatus.Text = "No alarm set.";
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduledActionService.Find("alarm") != null)
            {
                alarmStatus.Text = "Alarm removed.";
                alarmLogic.removeAlarm();
            }
            else
            {
                alarmStatus.Text = "No alarm to remove.";
            }

        }

    }
}