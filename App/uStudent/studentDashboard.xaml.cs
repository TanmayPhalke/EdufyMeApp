using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uStudent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class studentDashboard : ContentPage
    {
        public studentDashboard()
        {
            InitializeComponent();
            AttendanceImage.Source = ImageSource.FromResource("App.img.attendance.png");
            NoticeImage.Source = ImageSource.FromResource("App.img.na.png");
            AssignmentImage.Source = ImageSource.FromResource("App.img.assignments.png");
            NotesImage.Source = ImageSource.FromResource("App.img.notes.png");
            MarksImage.Source = ImageSource.FromResource("App.img.marks.png");
            ChangePasswordImage.Source = ImageSource.FromResource("App.img.changepass.png");
            LogoutImage.Source = ImageSource.FromResource("App.img.logout.png");
            LMSImage.Source = ImageSource.FromResource("App.img.extracurricular.png");
        }

        private void NoticeImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NoticePage());
        }

        private void AssignmentImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AssignmentPage());
        }

        private void DownloadNotesImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Browser.OpenAsync($"https://pro.medmee.in/DownloadNotes.aspx?BRANCH={Application.Current.Properties["BRANCH"]}", new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Hide
            });
        }

        private void AttendanceImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AttendancePage());
        }

        private void ChangePasswordImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new changePassword());
        }

        private void LogoutImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void MarksImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Marks());
        }

        private void LMSImageTapGestureRecognizer_Tapped(object sender,EventArgs e)
        {
            Navigation.PushAsync(new LearningCatalog());
        }
    }
}