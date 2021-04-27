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
    public partial class NoticeViewPage : ContentPage
    {
        NAClass na = new NAClass();
        public NoticeViewPage()
        {
            InitializeComponent();
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            List<NAClass> lnac = new List<NAClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/NA/GetNAforRole?NATYPE=NOTICE&FOR_ROLE={RolePicker.SelectedItem.ToString()}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<NAClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    NAClass nac = new NAClass()
                    {
                        CONTENT = JSON_DATA[i].CONTENT,
                        UDATE = JSON_DATA[i].UDATE
                    };
                    lnac.Add(nac);
                }

                NoticeList.ItemsSource = lnac;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }

        private async void NoticeList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var notice = e.Item as NAClass;
            bool result = await DisplayAlert("Do You Want To Delete This?", $"{notice.CONTENT}", "Yes", "No");

            if (result.Equals(true))
            {
                var values = new Dictionary<string, string>
                {
                    { "CONTENT", $"{notice.CONTENT}" }
                };
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(values);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var response = await client.PostAsync("https://api.medmee.in/NA/DeleteNA", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (JSON_DATA["STATUS"].ToString() == "DELETED")
                { await DisplayAlert("Message", "Notice Deleted Successfully", "Ok"); }
                else
                { await DisplayAlert("Error", "Fail To Delete Notice", "Ok"); }
            }
            else
            {
                
            }
        }
    }
}