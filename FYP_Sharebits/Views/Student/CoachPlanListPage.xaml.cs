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
    public partial class CoachPlanListPage : ContentPage
    {
        public CoachPlanListPage()
        {
            InitializeComponent();
        }

        private void PlanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}