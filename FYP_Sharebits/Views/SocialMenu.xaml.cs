using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    public partial class SocialMenu : ContentPage
    {
        public SocialMenu()
        {
            InitializeComponent();
        }

        private async void Push_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabitMenu());
        }

        private void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            SearchBarFrame.BackgroundColor = Color.White;
        }

        private void SearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            SearchBarFrame.BackgroundColor = Color.FromHex("#2196F3");
        }
    }
}