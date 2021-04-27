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
    public partial class StaffCreatePage : ContentPage
    {
        public StaffCreatePage()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (Branch_Picker.SelectedIndex >= 0 && Title_Picker.SelectedIndex >= 0)
            {
                if (SNAME_Entry.Text != string.Empty && EMAIL_Entry.Text != string.Empty)
                {
                    var values = new Dictionary<string, string>
                    {
                        { "ROLE", $"STAFF" },
                        { "NAME", $"{Title_Picker.SelectedItem.ToString()} {SNAME_Entry.Text.ToUpper()}" },
                        { "EMAIL", $"{EMAIL_Entry.Text.ToLower()}" },
                        { "MOBILE", $"{MOBILE_Entry.Text}" },
                        { "PASS", $"123" },
                        { "BRANCH", $"{Branch_Picker.SelectedItem.ToString()}" },
                        { "CY", $"null" },
                        { "PR", $"{EID_Entry.Text}" },
                        { "PHOTO", $"null" }
                    };
                    var client = new HttpClient();
                    var content = new FormUrlEncodedContent(values);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                    var response = await client.PostAsync("https://api.medmee.in/User/CheckAndCreateAccount", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                    if (JSON_DATA["STATUS"].ToString() == "CREATED")
                    { await DisplayAlert("Message", "Staff Creted Successfully", "Ok"); }
                    else
                    { await DisplayAlert("Error", "Fail To Create Staff. Email ID Already Exist", "Ok"); }
                }
                else
                {
                    await DisplayAlert("Error", "All Fields Are Required", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Select Branch and Title", "Ok");
            }
        }
    }
}