using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace App.uAdmin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectViewPage : ContentPage
    {
        public SubjectViewPage()
        {
            InitializeComponent();
        }

        private async void Search_Button_Clicked(object sender, EventArgs e)
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
                    SubjectClass SC = new SubjectClass ()
                    {
                        SNAME = JSON_DATA[i].SNAME,
                        STATUS = JSON_DATA[i].STATUS
                    };
                    lsc.Add(SC);
                }

                SubjectList.ItemsSource = lsc;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }

        private async void SubjectList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var subject = e.Item as SubjectClass;
            bool result = await DisplayAlert("Do You Want To Delete This?", $"{subject.SNAME}", "Yes", "No");

            if (result.Equals(true))
            {
                var values = new Dictionary<string, string>
                {
                    { "BNAME", $"{Branch_Picker.SelectedItem.ToString()}" },
                    { "SNAME", $"{subject.SNAME}" },
                    { "YY", $"{YY_Picker.SelectedItem.ToString()}" }
                };
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(values);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var response = await client.PostAsync("https://api.medmee.in/Subject/DeleteSubject", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (JSON_DATA["STATUS"].ToString() == "DELETED")
                { await DisplayAlert("Message", "Subject Deleted Successfully", "Ok"); }
                else
                { await DisplayAlert("Error", "Fail To Delete Subject", "Ok"); }
            }
            else
            {

            }
        }
    }
}