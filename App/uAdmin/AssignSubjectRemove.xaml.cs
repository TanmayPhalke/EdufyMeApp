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
    public partial class AssignSubjectRemove : ContentPage
    {
        public AssignSubjectRemove()
        {
            InitializeComponent();
            StaffName_TextBox.Text = Application.Current.Properties["staff_name"].ToString();
            StaffEmail_TextBox.Text = Application.Current.Properties["staff_email"].ToString();
        }

        private async void SubjectsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var subject = e.Item as StaffSubjectClass;
            bool result = await DisplayAlert("Do You Want To Delete This?", $"{subject.SUBNAME}", "Yes", "No");

            if (result.Equals(true))
            {
                var values = new Dictionary<string, string>
                    {
                        { "BRANCH", $"{Branch_Picker.SelectedItem.ToString()}" },
                        { "SID", $"{StaffEmail_TextBox.Text}" },
                        { "YY", $"{YY_Picker.SelectedItem.ToString()}" },
                        { "SUBJECT", $"{subject.SUBNAME}" }
                    };
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(values);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var response = await client.PostAsync("https://api.medmee.in/Subject/DeleteStaffSubject", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (JSON_DATA["STATUS"].ToString() == "DELETED")
                { await DisplayAlert("Message", "Staff Deleted Successfully", "Ok"); }
                else
                { await DisplayAlert("Error", "Fail To Delete Staff", "Ok"); }
            }
        }

        private async void YY_Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<StaffSubjectClass> lssc = new List<StaffSubjectClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/Subject/GetStaff_Subjects?SID={StaffEmail_TextBox.Text}&Branch={Branch_Picker.SelectedItem.ToString()}&YY={YY_Picker.SelectedItem.ToString()}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<StaffSubjectClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    StaffSubjectClass UC = new StaffSubjectClass()
                    {
                        SUBNAME = JSON_DATA[i].SUBNAME
                    };
                    lssc.Add(UC);
                }
                SubjectsList.ItemsSource = lssc;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }
    }
}