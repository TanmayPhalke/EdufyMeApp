using App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void Login_Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/User/ULogin?role={Role_Picker.SelectedItem.ToString()}&uid={UID_Entry.Text.ToLower()}&pass={PASS_Entry.Text}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                var UNAME = JSON_DATA["UNAME"].ToString();
                var STATUS = JSON_DATA["STATUS"].ToString();
                if (STATUS == "SUCCESS")
                {
                    Application.Current.Properties["UNAME"] = UNAME;
                    Application.Current.Properties["UID"] = UID_Entry.Text.ToLower();
                    Application.Current.Properties["PASS"] = PASS_Entry.Text.ToLower();
                    Application.Current.Properties["ROLE"] = Role_Picker.SelectedItem.ToString();

                    switch (Role_Picker.SelectedItem.ToString())
                    {

                        case "ADMIN":
                            await Navigation.PushAsync(new uAdmin.adminDashboard());
                            break;

                        case "STAFF":
                            await Navigation.PushAsync(new uStaff.staffDashboard());
                            break;

                        case "STUDENT":
                            // ADD BRANCH TO APPLICATION
                            {
                                var client1 = new HttpClient();
                                client1.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                                var uri1 = $"https://api.medmee.in/User/GetBranch_Year?UID={UID_Entry.Text.ToLower()}";
                                var result1 = await client.GetStringAsync(uri1);
                                var JSON_DATA1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(result1);
                                Application.Current.Properties["BRANCH"] = JSON_DATA1["BRANCH"];
                                Application.Current.Properties["YY"] = JSON_DATA1["CYEAR"];
                                await Navigation.PushAsync(new uStudent.studentDashboard());
                            }
                            break;
                    }

                }
                else
                {
                    await DisplayAlert("Error", "Invalid Login Credential", "Ok");
                }
        }
            catch (Exception ee)
            {
                await DisplayAlert("Error", "Invalid Login Credential", "Ok");
    }
}

private void ForgotPassword_Button_Clicked(object sender, EventArgs e)
{
    Navigation.PushAsync(new forgotPassword());
}
    }
}
