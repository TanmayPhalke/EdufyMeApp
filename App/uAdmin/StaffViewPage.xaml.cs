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
    public partial class StaffViewPage : ContentPage
    {
        public StaffViewPage()
        {
            InitializeComponent();
        }

        private async void Search_Button_Clicked(object sender, EventArgs e)
        {
            List<UserClass> lsc = new List<UserClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/User/GetUSER?Branch={Branch_Picker.SelectedItem.ToString()}&YY=null";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<UserClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    UserClass UC = new UserClass()
                    {
                        UNAME = JSON_DATA[i].UNAME,
                        EMAIL = JSON_DATA[i].EMAIL,
                        MOBILE = JSON_DATA[i].MOBILE
                    };
                    lsc.Add(UC);
                }
                StaffList.ItemsSource = lsc;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }
        public async void StaffList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var USER = e.Item as UserClass;
            //bool result = await DisplayAlert("Do You Want To Delete This?", $"{USER.UNAME}", "Yes", "No");
            string result = await DisplayActionSheet("What You Wan To Do?", "Cancel", null, "Assign Subject", "Delete Staff");

            switch (result)
            {
                case "Assign Subject":
                    Application.Current.Properties["staff_name"] = USER.UNAME;
                    Application.Current.Properties["staff_email"] = USER.EMAIL;
                    await Navigation.PushAsync(new uAdmin.AssignSubjectTabbedPage());
                    break;

                case "Delete Staff":
                    var values = new Dictionary<string, string>
                    {
                        { "BRANCH", $"{Branch_Picker.SelectedItem.ToString()}" },
                        { "EMAIL", $"{USER.EMAIL}" },
                    };
                    var client = new HttpClient();
                    var content = new FormUrlEncodedContent(values);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                    var response = await client.PostAsync("https://api.medmee.in/User/DeleteUser", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                    if (JSON_DATA["STATUS"].ToString() == "DELETED")
                    { await DisplayAlert("Message", "Staff Deleted Successfully", "Ok"); }
                    else
                    { await DisplayAlert("Error", "Fail To Delete Staff", "Ok"); }
                    break;
            }

        }
    }
}