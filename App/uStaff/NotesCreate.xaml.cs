using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uStaff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesCreate : ContentPage
    {
        public NotesCreate()
        {
            InitializeComponent();
            var url = $"https://pro.medmee.in/UploadNotes.aspx?UID={Application.Current.Properties["UID"]}&UNAME={Application.Current.Properties["UNAME"]}";
            MyBrowser.Source = HttpUtility.HtmlEncode(url);
        }

        private void ModifyNotes_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new NotesDelete());
            Browser.OpenAsync($"https://pro.medmee.in/ModifyNotes.aspx?UID={Application.Current.Properties["UID"]}&UNAME={Application.Current.Properties["UNAME"]}", new BrowserLaunchOptions
           {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Hide
            });
        }
    }
}