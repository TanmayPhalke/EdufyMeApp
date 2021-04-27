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

namespace App.uStudent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Marks : ContentPage
    {
        public Marks()
        {
            InitializeComponent();
        }

        private async void ViewMarks_Button_Clicked(object sender, EventArgs e)
        {
            List<MarksClass> lnac = new List<MarksClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/Marks/DisplayResult?SID={Application.Current.Properties["UID"]}&ResultType={ExamType_Picker.SelectedItem}";
                var result = await client.GetStringAsync(uri);
                var JSON_DATA = JsonConvert.DeserializeObject<List<MarksClass>>(result);
                var COUNT = JSON_DATA.Count;

                for (int i = 0; i < COUNT; i++)
                {
                    MarksClass mr = new MarksClass()
                    {
                        SUBJECT = JSON_DATA[i].SUBJECT,
                        MARKS_OBTAIN = JSON_DATA[i].MARKS_OBTAIN,
                        MARKS_OUTOF = JSON_DATA[i].MARKS_OUTOF,
                        STATUS = JSON_DATA[i].STATUS,
                    };
                    lnac.Add(mr);
                }

                Marks_List.ItemsSource = lnac;
            }
            catch (Exception ee)
            {
                await DisplayAlert("Message", "No Data To Show", "Ok");
            }
        }
    }
}