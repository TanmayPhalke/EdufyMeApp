using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uStaff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesDelete : ContentPage
    {
        public NotesDelete()
        {
            InitializeComponent();
            var url = $"https://pro.medmee.in/ModifyNotes.aspx?UID={Application.Current.Properties["UID"]}&UNAME={Application.Current.Properties["UNAME"]}";
            MyBrowser.Source = HttpUtility.HtmlEncode(url);
        }
    }
}