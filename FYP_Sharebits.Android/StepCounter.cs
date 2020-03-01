using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FYP_Sharebits.Droid;
using FYP_Sharebits.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(StepCounter))]
namespace FYP_Sharebits.Droid
{
    class StepCounter : Java.Lang.Object, IStepCounter, ISensorEventListener
    {

        private int StepsCounter = 0;
        private SensorManager sManager;

        public int Steps
        {
            get { return StepsCounter; }
            set { StepsCounter = value; }
        }

        public new void Dispose()
        {
            sManager.UnregisterListener(this);
            sManager.Dispose();
        }

        public void InitSensorService()
        {

            sManager = Android.App.Application.Context.GetSystemService(Context.SensorService) as SensorManager;

            sManager.RegisterListener(this, sManager.GetDefaultSensor(SensorType.StepDetector), SensorDelay.Fastest);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {


            Console.WriteLine("OnAccuracyChanged called");
        }

        public void OnSensorChanged(SensorEvent e)
        {
            StepsCounter++;



            Console.WriteLine(e.ToString());
        }

        public void StopSensorService()
        {
            sManager.UnregisterListener(this);
        }

        public bool IsAvailable()
        {
            return Android.App.Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter) && Android.App.Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);
        }
    }
}