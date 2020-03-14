using FYP_Sharebits.Interfaces;
using FYP_Sharebits.Models.Functional;
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
    public partial class TestStepCounter : ContentPage
    {
        public TestStepCounter()
        {
            InitializeComponent();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            mylabel.Text = "Count in DB: " + (await StepController.GetTodaySteps_DB()).ToString();

            mylabel2.Text = "Count in Properties: " + (await StepController.GetTodaySteps_Properties()).ToString();

            mylabel3.Text = "Count in StepCounter.cs: " + (DependencyService.Get<IStepCounter>().Steps);

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await StepController.UpdateTodaySteps_DB();
            await StepController.UpdateTodaySteps_Properties();
        }
    }
}