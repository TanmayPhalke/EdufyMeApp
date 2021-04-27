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
    public partial class NoticeCreatePage : ContentPage
    {
        public NoticeCreatePage()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (NoticeEditor.Text != "")
            {
                var values = new Dictionary<string, string>
                {
                    { "NATYPE", $"NOTICE" },
                    { "ROLE", $"ADMIN" },
                    { "ID", $"{Application.Current.Properties["UID"].ToString()}" },
                    { "UTYPE", $"{RolePicker.SelectedItem.ToString()}" },
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
                { await DisplayAlert("Message", "Notice Creted Successfully", "Ok"); }
                else
                { await DisplayAlert("Error", "Fail To Create Notice Crete", "Ok"); }
            }
            else
            {
                await DisplayAlert("Error", "Notice Cannot Be Null...", "Ok");
                NoticeEditor.Focus();
            }
        }
    }
}