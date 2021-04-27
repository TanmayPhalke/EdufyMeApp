using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uStaff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaffStudentAnalysisPage : ContentPage
    {
        public StaffStudentAnalysisPage()
        {
            InitializeComponent();
            StudAnalysisImgLogo.Source = ImageSource.FromResource("App.img.extracurricular.png");
        }

        private void SubjectwiseAttendanceBtClicked(Object e, EventArgs eventArgs)
        {
            var url = "https://prod-apnortheast-a.online.tableau.com/t/edufymesolutionforsmartcollege/views/EDUFYME/ATTENDANCE?:showAppBanner=false&:display_count=n&:showVizHome=n&:origin=viz_share_link";
            AnalysisWebView.Source = HttpUtility.HtmlEncode(url);
            AnalysisWebView.IsVisible = true;
            NavBt.IsVisible = false;

        }
        private void DepartmentwiseAttendanceBtClicked(Object e, EventArgs eventArgs)
        {

            var url = "https://prod-apnortheast-a.online.tableau.com/t/edufymesolutionforsmartcollege/views/EDUFYME/DEPARTMENT?:showAppBanner=false&:display_count=n&:showVizHome=n&:origin=viz_share_link";
            AnalysisWebView.Source = HttpUtility.HtmlEncode(url);
            AnalysisWebView.IsVisible = true;
            NavBt.IsVisible = false;

        }
        private void StudMarksBtClicked(Object e, EventArgs eventArgs)
        {

            var url = "https://prod-apnortheast-a.online.tableau.com/t/edufymesolutionforsmartcollege/views/MARKIE/Dashboard1?:showAppBanner=false&:display_count=n&:showVizHome=n&:origin=viz_share_link";
            AnalysisWebView.Source = HttpUtility.HtmlEncode(url);
            AnalysisWebView.IsVisible = true;
            NavBt.IsVisible = false;
        }
    }
}