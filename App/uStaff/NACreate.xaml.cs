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
    public partial class NACreate : ContentPage
    {
        public NACreate()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (NoticeEditor.Text != "")
            {
                var values = new Dictionary<string, string>
                {
                    { "NATYPE", $"{NAType_Picker.SelectedItem.ToString()}" },
                    { "ROLE", $"STAFF" },
                    { "ID", $"{Application.Current.Properties["UID"].ToString()}" },
                    { "UTYPE", $"STUDENT" },
                    { "CONTENT", $"{NoticeEditor.Text}" },
                    { "DD", $"{DateTime.Now.Day}" },
                    { "MM", $"{DateTime.Now.Month}" },
                    { "YY", $"{DateTime.Now.Year}" }
                };
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(values);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var response = await client.PostAsync("https://api.medmee.in/NA/CreateNA", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (JSON_DATA["STATUS"].ToString() == "CREATED")
                { await DisplayAlert("Message", $"{NAType_Picker.SelectedItem.ToString()} Creted Successfully", "Ok"); }
                else
                { await DisplayAlert("Error", "Fail To Create Notice Crete", "Ok"); }
            }
            else
            {
                await DisplayAlert("Error", $"{NAType_Picker.SelectedItem.ToString()} Cannot Be Null...", "Ok");
                NoticeEditor.Focus();
            }
        }
    }
}