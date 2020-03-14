using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FYP_Sharebits.Data;
using System.IO;
using FYP_Sharebits.Interfaces;

namespace FYP_Sharebits
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new MainTabbedPage();


            if (DependencyService.Get<IStepCounter>().IsAvailable())
            {
                DependencyService.Get<IStepCounter>().InitSensorService();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        static SharebitsDB database;

        public static SharebitsDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new SharebitsDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sharebits.db3"));
                }
                return database;
            }
        }
    }
}
