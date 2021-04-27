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
    public partial class AssignmentPage : ContentPage
    {
        public AssignmentPage()
        {
            InitializeComponent();
            GetAssignment();
        }

        public async void GetAssignment()
        {
            List<NAClass> lnac = new List<NAClass>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var uri = $"https://api.medmee.in/NA/GetNAforRole?NATYPE=ASSIGNMENT&FOR_ROLE=STUDENT";
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
    }
}