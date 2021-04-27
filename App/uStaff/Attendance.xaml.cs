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

namespace App.uStaff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Attendance : ContentPage
    {
        public Attendance()
        {
            InitializeComponent();
            GetAllBranches();
            GetAllYears();
        }

        public async void GetAllBranches()
        {
            List<StaffSubjectClass> lsc = new List<StaffSubjectClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/Subject/GetAllBranch?STID={Application.Current.Properties["UID"].ToString()}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<StaffSubjectClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    StaffSubjectClass ssc = new StaffSubjectClass()
                    {
                        BRANCH = JSON_DATA[i].BRANCH
                    };
                    Branch_Picker.Items.Add(ssc.BRANCH);
                }                
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }

        public async void GetAllYears()
        {
            List<StaffSubjectClass> lsc = new List<StaffSubjectClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/Subject/GetAllYears?STID={Application.Current.Properties["UID"].ToString()}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<StaffSubjectClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    StaffSubjectClass ssc = new StaffSubjectClass()
                    {
                        YY = JSON_DATA[i].YY
                    };
                    YY_Picker.Items.Add(ssc.YY);
                }                
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }


        private async void YY_Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<StaffSubjectClass> lsc = new List<StaffSubjectClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/Subject/GetStaff_Subjects?SID={Application.Current.Properties["UID"].ToString()}&Branch={Branch_Picker.SelectedItem.ToString()}&YY={YY_Picker.SelectedItem.ToString()}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<StaffSubjectClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    StaffSubjectClass SC = new StaffSubjectClass()
                    {
                        SUBNAME = JSON_DATA[i].SUBNAME,
                    };
                    //lsc.Add(SC);

                    Subject_Picker.Items.Add(SC.SUBNAME);
                }

                //Subject_Picker.ItemsSource = lsc;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }

        private async void Subject_Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<UserClass> lsc = new List<UserClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/User/GetUSER?Branch={Branch_Picker.SelectedItem.ToString()}&YY={YY_Picker.SelectedItem.ToString()}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<UserClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    UserClass UC = new UserClass()
                    {
                        UNAME = JSON_DATA[i].UNAME, 
                        EMAIL = JSON_DATA[i].EMAIL,
                        PE = JSON_DATA[i].PE
                    };
                    lsc.Add(UC);
                }
                StudentList.ItemsSource = lsc;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }

        private async void StudentList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var student = e.Item as UserClass;
            string result =await DisplayActionSheet($"Is Student {student.UNAME} Present", "Ok", null, "PRESENT", "ABSCENT");

            var values = new Dictionary<string, string>
                {
                    { "BRANCH", $"{Branch_Picker.SelectedItem.ToString()}"},
                    { "YY", $"{YY_Picker.SelectedItem.ToString()}"},
                    { "SUBJECT", $"{Subject_Picker.SelectedItem.ToString()}"},
                    { "ATTENDANCE", $"{result}"},
                    { "ID", $"{Application.Current.Properties["UID"].ToString()}"},
                    { "SNAME", $"{Application.Current.Properties["UNAME"].ToString()}"},
                    { "SUID", $"{student.EMAIL}"},
                    { "SUNAME", $"{student.UNAME}"},
                    { "DD", $"{DateTime.Now.Day}"},
                    { "MM", $"{DateTime.Now.Month}"},
                    { "YYYY", $"{DateTime.Now.Year}"}
                };
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            var response = await client.PostAsync("https://api.medmee.in/Presenty/addAttendance", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

            if (JSON_DATA["STATUS"].ToString() == "P")
            { await DisplayAlert("Message", $"Student Attendence Registered Successfully as {result}", "Ok"); }
            else
            { await DisplayAlert("Error", "Fail To Register", "Ok"); }

        }
    }
}