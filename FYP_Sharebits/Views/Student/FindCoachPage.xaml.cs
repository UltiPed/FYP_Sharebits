using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindCoachPage : ContentPage
    {
        public FindCoachPage()
        {
            InitializeComponent();
        }

        private void CoachListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}