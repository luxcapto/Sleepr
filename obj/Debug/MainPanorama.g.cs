﻿#pragma checksum "C:\Users\luxcapto\Dropbox\Classes\ITM 455\Final Project\Sleepr\Sleepr\MainPanorama.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D497C926B2B23C31B54E3244FBCDF6D2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;


namespace Sleepr {
    
    
    public partial class MainPanorama : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama panoramaView;
        
        internal Telerik.Windows.Controls.RadListPicker timeOptionPicker;
        
        internal Telerik.Windows.Controls.RadTimePicker timePicker;
        
        internal Telerik.Windows.Controls.RadListPicker minutePicker;
        
        internal System.Windows.Controls.Button deleteButton;
        
        internal System.Windows.Controls.TextBlock helperText;
        
        internal System.Windows.Controls.TextBlock alarmStatus;
        
        internal Telerik.Windows.Controls.RadDataBoundListBox radDataBoundListBox;
        
        internal System.Windows.Controls.TextBlock alarmHelperText;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Sleepr;component/MainPanorama.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.panoramaView = ((Microsoft.Phone.Controls.Panorama)(this.FindName("panoramaView")));
            this.timeOptionPicker = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("timeOptionPicker")));
            this.timePicker = ((Telerik.Windows.Controls.RadTimePicker)(this.FindName("timePicker")));
            this.minutePicker = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("minutePicker")));
            this.deleteButton = ((System.Windows.Controls.Button)(this.FindName("deleteButton")));
            this.helperText = ((System.Windows.Controls.TextBlock)(this.FindName("helperText")));
            this.alarmStatus = ((System.Windows.Controls.TextBlock)(this.FindName("alarmStatus")));
            this.radDataBoundListBox = ((Telerik.Windows.Controls.RadDataBoundListBox)(this.FindName("radDataBoundListBox")));
            this.alarmHelperText = ((System.Windows.Controls.TextBlock)(this.FindName("alarmHelperText")));
        }
    }
}

