using FYP_Sharebits.Interfaces;
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

            if (DependencyService.Get<IStepCounter>().IsAvailable())
            {
                DependencyService.Get<IStepCounter>().InitSensorService();

                myBtn.IsVisible = true;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {


            mylabel.Text = DependencyService.Get<IStepCounter>().Steps.ToString();



        }
    }
}