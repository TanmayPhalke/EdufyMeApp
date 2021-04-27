using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uAdmin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class adminDashboard : ContentPage
    {
        public adminDashboard()
        {
            InitializeComponent();
                NoticeImage.Source = ImageSource.FromResource("App.img.na.png");
                SubjectImage.Source = ImageSource.FromResource("App.img.subject.png");
                StaffImage.Source = ImageSource.FromResource("App.img.staff.png");
                //AssignSubjectImage.Source = ImageSource.FromResource("App.img.records.png");
                ChangePasswordImage.Source = ImageSource.FromResource("App.img.changepass.png");
                LogoutImage.Source = ImageSource.FromResource("App.img.logout.png");
        }        

        private void NoticeImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NoticeTabbedPage());
        }

        private void SubjectImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SubjectTabbedPage());
        }

        private void StaffImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StaffTabbedPage());
        }

        //private void AssignSubjectImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new NoticeTabbedPage());
        //}

        private void ChangePasswordImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new changePassword());
        }

        private void LogoutImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}