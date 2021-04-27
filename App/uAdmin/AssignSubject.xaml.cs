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

namespace App.uAdmin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignSubject : ContentPage
    {
        public AssignSubject()
        {
            InitializeComponent();
            StaffName_TextBox.Text = Application.Current.Properties["staff_name"].ToString();
            StaffEmail_TextBox.Text = Application.Current.Properties["staff_email"].ToString();
        }

        //private async void Search_Button_Clicked(object sender, EventArgs e)
        //{
        //    List<SubjectClass> lsc = new List<SubjectClass>();
        //    try
        //    {
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
        //        var uri = $"https://api.medmee.in/Subject/Get_All_Subjects?BNAME={Branch_Picker.SelectedItem.ToString()}&YY={YY_Picker.SelectedItem.ToString()}";
        //        var result = await client.GetStringAsync(uri);
        //        var JSON_DATA = JsonConvert.DeserializeObject<List<SubjectClass>>(result);
        //        var COUNT = JSON_DATA.Count;

        //        for (int i = 0; i < COUNT; i++)
        //        {
        //            SubjectClass SC = new SubjectClass()
        //            {
        //                SNAME = JSON_DATA[i].SNAME,                        
        //            };
        //            //lsc.Add(SC);

        //            Subject_Picker.Items.Add(SC.SNAME);
        //        }

        //        //Subject_Picker.ItemsSource = lsc;
        //    }
        //    catch (Exception ee)
        //    {
        //        await DisplayAlert("Message", "No Data To Show", "Ok");
        //    }
        //}

        private async void Submit_Button_Clicked(object sender, EventArgs e)
        {
            var values = new Dictionary<string, string>
                    {
                        { "SNAME", $"{StaffName_TextBox.Text}" },
                        { "SID", $"{StaffEmail_TextBox.Text}" },
                        { "BRANCH", $"{Branch_Picker.SelectedItem.ToString()}" },
                        { "YY", $"{YY_Picker.SelectedItem.ToString()}" },
                        { "SUBJECT", $"{Subject_Picker.SelectedItem.ToString()}" }
                    };
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            var response = await client.PostAsync("https://api.medmee.in/Subject/AssignStaffSubject", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

            if (JSON_DATA["STATUS"].ToString() == "CREATED")
            { await DisplayAlert("Message", "Subject Allocated Successfully", "Ok"); }
            else
            { await DisplayAlert("Error", "Fail To Allocate Subject. Subject Already Exist", "Ok"); }
        }

        private async void YY_Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SubjectClass> lsc = new List<SubjectClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/Subject/Get_All_Subjects?BNAME={Branch_Picker.SelectedItem.ToString()}&YY={YY_Picker.SelectedItem.ToString()}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<SubjectClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    SubjectClass SC = new SubjectClass()
                    {
                        SNAME = JSON_DATA[i].SNAME,
                    };
                    //lsc.Add(SC);

                    Subject_Picker.Items.Add(SC.SNAME);
                }

                //Subject_Picker.ItemsSource = lsc;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }

        //private async void Branch_Picker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    List<SubjectClass> lsc = new List<SubjectClass>();
        //    try
        //    {
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
        //        var uri = $"https://api.medmee.in/Subject/Get_All_Subjects?BNAME={Branch_Picker.SelectedItem.ToString()}&YY={YY_Picker.SelectedItem.ToString()}";
        //        var result = await client.GetStringAsync(uri);
        //        var JSON_DATA = JsonConvert.DeserializeObject<List<SubjectClass>>(result);
        //        var COUNT = JSON_DATA.Count;

        //        for (int i = 0; i < COUNT; i++)
        //        {
        //            SubjectClass SC = new SubjectClass()
        //            {
        //                SNAME = JSON_DATA[i].SNAME,
        //            };
        //            //lsc.Add(SC);

        //            Subject_Picker.Items.Add(SC.SNAME);
        //        }

        //        //Subject_Picker.ItemsSource = lsc;
        //    }
        //    catch (Exception ee)
        //    {
        //        await DisplayAlert("Message", "No Data To Show", "Ok");
        //    }
        //}
    }
}