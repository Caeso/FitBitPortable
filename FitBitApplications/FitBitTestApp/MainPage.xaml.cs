using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Windows.Security.Authentication.Web;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Windows.Web;

// using WinRTXamlToolkit.Controls.DataVisualization.Charting;

using Newtonsoft.Json;

using System.Threading.Tasks;

using FitBitPortable;
using FitBitPortable.DataModels;
using FitBitPortable.Exceptions;
using FitBitPortable.OAuth2;

using FitBit;

namespace FitBitPersonalTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        FitBitClient _Client;

        DateTimeOffset _Start;
        DateTimeOffset _End;

        public MainPage()
        {
            this.InitializeComponent();

            web.NavigationCompleted += Web_NavigationCompleted;
            web.NavigationFailed += Web_NavigationFailed;

             _Client = new FitBitClient();

            SolidColorBrush white = new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
            signalNewAuthentication.Fill = white;

            DateTimeOffset _now = DateTimeOffset.Now;

            _Start = new DateTimeOffset(_now.Year, _now.Month, _now.Day, 0, 0, 0, new TimeSpan(0));
            _End = new DateTimeOffset(_now.Year, _now.Month, _now.Day, 23, 59, 59, new TimeSpan(0));

            DatePickerStart.Date = _Start;
            DatePickerEnd.Date = _End;

            flySetTime.Time = new TimeSpan(_Start.Hour, _Start.Minute, _Start.Second);
            flyEndTime.Time = new TimeSpan(_End.Hour, _End.Minute, _End.Second);

            buttonStartTime.Content = flySetTime.Time.ToString(@"hh\:mm");
            buttonEndTime.Content = flyEndTime.Time.ToString(@"hh\:mm");

            PrintStartAndEnd();
        }

        private void Web_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            textBox.Text = "Web navigation failed!";
        }

        private void Web_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            textBox.Text = "Web navigation completed!";
        }

        async Task GetData()
        {
            List<HeartRateIntradayTimeSeries> HeartRateList = await _Client.GetHeartRateIntradayTimeSeries(_Start, _End, HeartRateIntradayDetailLevel.DetailLevel1sec);

            textBox2.Text = "Anzahl Datensätze: " + HeartRateList.Count.ToString();

            /*
            int count = 0;
            float average = 0f;
            int average_count = 0;
            int modulo = HeartRateList.Count < 100 ? 1 : HeartRateList.Count / 100;
            List<FitBitClient.HeartRateData> temp = new List<FitBitClient.HeartRateData>();
            List<FitBitClient.HeartRateData> temp_avg = new List<FitBitClient.HeartRateData>();
            foreach (FitBitClient.HeartRateData x in HeartRateList)
            {
                average += x.HeartRate;
                average_count++;

                if (count++ % modulo == 0)
                {
                    temp_avg.Add(new FitBitClient.HeartRateData() { Time = x.Time, HeartRate = average / average_count });

                    average = 0;
                    average_count = 0;

                    temp.Add(x);
                }
            }

            (LineChart.Series[0] as LineSeries).IndependentValuePath = FitBitClient.HeartRateData.NameX;
            (LineChart.Series[0] as LineSeries).DependentValuePath = FitBitClient.HeartRateData.NameY;
            (LineChart.Series[0] as LineSeries).ItemsSource = temp;

            (LineChart.Series[1] as LineSeries).IndependentValuePath = FitBitClient.HeartRateData.NameX;
            (LineChart.Series[1] as LineSeries).DependentValuePath = FitBitClient.HeartRateData.NameY;
            (LineChart.Series[1] as LineSeries).ItemsSource = temp_avg;
            */

            return;
        }

        private async void buttonAuthentication_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush red = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
            SolidColorBrush green = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0));

            buttonGetData.IsEnabled = false;
            buttonAuthentication.IsEnabled = false;

            _Client.Restore();

            bool auth = await _Client.RefreshToken();
            if (!auth) auth = await _Client.Authentication();

            _Client.Save();

            signalNewAuthentication.Fill = auth ? green : red;

            buttonGetData.IsEnabled = true;
            buttonAuthentication.IsEnabled = true;
        }

        private async void buttonGetData_Click(object sender, RoutedEventArgs e)
        {
            buttonGetData.IsEnabled = false;
            buttonAuthentication.IsEnabled = false;

            await GetData();

            buttonGetData.IsEnabled = true;
            buttonAuthentication.IsEnabled = true;
        }

        private async void buttonSaveData_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker FilePicker = new FileSavePicker();
            FilePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            FilePicker.FileTypeChoices.Add("Plain Text", new List<String>() { ".txt" });
            FilePicker.SuggestedFileName = "FitBitData";

            StorageFile File = await FilePicker.PickSaveFileAsync();

            if (File != null) textBox.Text = File.Path + File.Name + "(" + File.DisplayName + ")";
        }

        private async void buttonDeviceData_Click(object sender, RoutedEventArgs e)
        {
            List<DeviceData> DeviceData = await _Client.GetDeviceData();

            textBox.Text = "Device Id     : " + DeviceData[0].Id + Environment.NewLine;
            textBox.Text += "Device Version: " + DeviceData[0].DeviceVersion + Environment.NewLine;
            textBox.Text += "Type          : " + DeviceData[0].Type + Environment.NewLine;
            textBox.Text += "Battery Status: " + DeviceData[0].Battery + Environment.NewLine;
            textBox.Text += "Last Sync Time: " + DeviceData[0].LastSyncTime + Environment.NewLine;
        }

        private void TimePickerFlyout_StartTimePicked(TimePickerFlyout sender, TimePickedEventArgs e)
        {
            _Start = new DateTime(_Start.Year, _Start.Month, _Start.Day, e.NewTime.Hours, e.NewTime.Minutes, e.NewTime.Seconds);
            buttonStartTime.Content = _Start.TimeOfDay.ToString(@"hh\:mm");
            PrintStartAndEnd();
        }

        private void TimePickerFlyout_EndTimePicked(TimePickerFlyout sender, TimePickedEventArgs e)
        {
            _End = new DateTime(_End.Year, _End.Month, _End.Day, e.NewTime.Hours, e.NewTime.Minutes, e.NewTime.Seconds);
            buttonEndTime.Content = _End.TimeOfDay.ToString(@"hh\:mm");
            PrintStartAndEnd();
        }

        private void DatePickerStart_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            _Start = new DateTime(args.NewDate.Value.Year, args.NewDate.Value.Month, args.NewDate.Value.Day, _Start.Day, _Start.Hour, _Start.Minute, _Start.Second);
            PrintStartAndEnd();
        }

        private void DatePickerEnd_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            _End = new DateTime(args.NewDate.Value.Year, args.NewDate.Value.Month, args.NewDate.Value.Day, _End.Day, _End.Hour, _End.Minute, _End.Second);
            PrintStartAndEnd();

        }

        void PrintStartAndEnd()
        {
            textBox.Text = "Start: " + _Start.ToString() + Environment.NewLine;
            textBox.Text += "End: " + _End.ToString() + Environment.NewLine;
        }

        private async void buttonActivities_Click(object sender, RoutedEventArgs e)
        {
            UserProfile user = await _Client.GetUserProfile();

            textBox.Text = user.DisplayName + "," + user.Age.ToString() + "," + user.City + "," + user.Country + Environment.NewLine;

            await _Client.GetActivitySummary(DateTimeOffset.Now);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            /*
            List<FitBitPortable.DataModels.DeviceData> list = await _Client.GetDeviceData();

            FitBitPortable.DataModels.UserProfile user = await _Client.GetUserProfile();

            FitBitPortable.DataModels.ActivitySummary summary = await _Client.GetActivitySummary(new DateTime(2016, 4, 12));

            List<FitBitPortable.DataModels.HeartRateIntradayTimeSeries> heartrate =
                await _Client.GetHeartRateIntradayTimeSeries(
                    new DateTime(2016, 4, 12, 0, 0, 0),
                    new DateTime(2016, 4, 12, 1, 0, 0),
                    FitBitPortable.DataModels.HeartRateIntradayDetailLevel.DetailLevel1sec);

            FitBitPortable.DataModels.HeartRateTimeSeries heart =
                await _Client.GetHeartRateTimeSeries(
                    new DateTime(2016, 4, 10, 0, 0, 0),
                    new DateTime(2016, 4, 17, 0, 0, 0));
            */
            ActivityTimeSeries calories, steps, distance, floors, elevation, steps2;

            List<ActivityIntradayTimeSeries> x = await _Client.GetActivityIntradayTimeSeries(ActivitiyTimeSeriesResource.Calories, new DateTime(2016, 4, 10, 0, 0, 0), new DateTime(2016, 4, 12, 0, 0, 0), ActivityIntradayDetailLevel.DetailLevel1min);
        }

        private void X_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            textBox.Text += "Property 1 changed: " + e.PropertyName + Environment.NewLine;
        }
    }
}
