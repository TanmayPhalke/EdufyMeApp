using App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uStudent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendancePage : ContentPage
    {
        public AttendancePage()
        {
            InitializeComponent();
            GetMyAttendance();
        }

        public async void GetMyAttendance()
        {
            List<CountPresentyClass> lnac = new List<CountPresentyClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/Presenty/GetAttendenceCount?BRANCH={Application.Current.Properties["BRANCH"]}&YY={Application.Current.Properties["YY"]}&SID={Application.Current.Properties["UID"]}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<CountPresentyClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    CountPresentyClass nac = new CountPresentyClass()
                    {
                        Presenty_Percentage = JSON_DATA[i].Presenty_Percentage,
                        SubjectName = JSON_DATA[i].SubjectName,
                        Total_Lectures = JSON_DATA[i].Total_Lectures,
                        Total_Present = JSON_DATA[i].Total_Present
                    };
                    lnac.Add(nac);
                }

                Attendance_List.ItemsSource = lnac;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }
    }
}