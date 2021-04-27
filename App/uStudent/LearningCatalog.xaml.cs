using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.uStudent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearningCatalog : ContentPage
    {
        public LearningCatalog()
        {
            InitializeComponent();
        }

        private void MLButton_Clicked(object s, EventArgs e)
        {
            var mlurl = "https://www.youtube.com/watch?v=GwIo3gDZCVQ";
            Browser.Source = HttpUtility.HtmlEncode(mlurl);
            CourseStack.IsVisible = false;
            Browser.IsVisible = true;
            LabelStack.IsVisible = false;
        }

        private void DSButton_Clicked(object s, EventArgs e)
        {
            var dsurl = "https://www.youtube.com/watch?v=B31LgI4Y4DQ";
            Browser.Source = HttpUtility.HtmlEncode(dsurl);
            CourseStack.IsVisible = false;
            Browser.IsVisible = true;
            LabelStack.IsVisible = false;
        }

        private void DSciButtonClicked(object s, EventArgs e)
        {
            var dscurl = "https://www.youtube.com/watch?v=-ETQ97mXXF0";
            Browser.Source = HttpUtility.HtmlEncode(dscurl);
            CourseStack.IsVisible = false;
            Browser.IsVisible = true;
            LabelStack.IsVisible = false;
        }

        private void GetStartedButton_Clicked(object s, EventArgs e)
        {

            var lurl = "https://www.youtube.com/c/Freecodecamp/playlists";
            Browser.Source = HttpUtility.HtmlEncode(lurl);
            CourseStack.IsVisible = false;
            Browser.IsVisible = true;
            LabelStack.IsVisible = false;

        }
    }
}