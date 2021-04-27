using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class changePassword : ContentPage
    {
        public changePassword()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (CurrentPass_Entry.Text == Application.Current.Properties["PASS"].ToString())
            {
                var values = new Dictionary<string, string>
                    {
                        { "ROLE", $"{Application.Current.Properties["ROLE"]}" },
                        { "EMAIL",$"{Application.Current.Properties["UID"]}"},
                        {"NPASS",$"{ConfPass_Entry.Text}" }

                    };
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(values);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var response = await client.PostAsync("https://api.medmee.in/User/ChangePassword", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (JSON_DATA["STATUS"].ToString() == "UPDATED")
                { 
                    await DisplayAlert("Message", "Password Changed Successfully", "Ok");
                    Application.Current.Properties["PASS"] = ConfPass_Entry.Text;
                    ConfPass_Entry.Text = "";
                    CurrentPass_Entry.Text = "";
                    NewPass_Entry.Text = "";
                }
                else
                { await DisplayAlert("Error", "Connection Error", "Ok"); }
            }
            else
            {
                await DisplayAlert("Error", "Invalid Current Password", "Ok");
                CurrentPass_Entry.Focus();
            }
        }
    }
}