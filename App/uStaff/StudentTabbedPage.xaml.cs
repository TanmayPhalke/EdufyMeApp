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
    public partial class StudentTabbedPage : TabbedPage
    {
        public StudentTabbedPage()
        {
            InitializeComponent();
        }
    }
}