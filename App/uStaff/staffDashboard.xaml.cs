using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uStaff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class staffDashboard : ContentPage
    {
        public staffDashboard()
        {
            InitializeComponent();
            AttendanceImage.Source = ImageSource.FromResource("App.img.attendance.png");
            NoticeImage.Source = ImageSource.FromResource("App.img.assignments.png");
            UploadNotesImage.Source = ImageSource.FromResource("App.img.notes.png");
            CreateStudentImage.Source = ImageSource.FromResource("App.img.student.png");
            ChangePasswordImage.Source = ImageSource.FromResource("App.img.changepass.png");
            LogoutImage.Source = ImageSource.FromResource("App.img.logout.png");
            MarksImage.Source = ImageSource.FromResource("App.img.marks.png");
            StudentAnalysisImg.Source = ImageSource.FromResource("App.img.extracurricular.png");
        }

        private void AttendanceImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uStaff.Attendance());
        }

        private void NoticeImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uStaff.NATabbedPage());
        }

        private void UploadNotesImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uStaff.NotesCreate());
        }

        private void CreateStudentImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uStaff.StudentTabbedPage());
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
            Navigation.PushAsync(new MarksViewStudent());
        }

        private void StudentAnalysisImageTapRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StaffStudentAnalysisPage());
        }
    }
}