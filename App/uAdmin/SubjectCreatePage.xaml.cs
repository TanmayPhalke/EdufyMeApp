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
    public partial class SubjectCreatePage : ContentPage
    {
        public SubjectCreatePage()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (Branch_Picker.SelectedIndex >= 0 && YY_Picker.SelectedIndex >= 0)
            {
                var values = new Dictionary<string, string>
                {
                    { "BNAME", $"{Branch_Picker.SelectedItem}" },
                    { "SNAME", $"{Subject_Entry.Text.ToUpper()}" },
                    { "YY", $"{YY_Picker.SelectedItem.ToString()}" }
                    
                };
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(values);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var response = await client.PostAsync("https://api.medmee.in/Subject/CheckAndCreateSubject", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (JSON_DATA["STATUS"].ToString() == "CREATED")
                { await DisplayAlert("Message", "Subject Creted Successfully", "Ok"); }
                else
                { await DisplayAlert("Error", "Fail To Create Subject", "Ok"); }
            }
            else
            {
                await DisplayAlert("Error", "Select Branch and Year", "Ok");                
            }
        }
    }
}