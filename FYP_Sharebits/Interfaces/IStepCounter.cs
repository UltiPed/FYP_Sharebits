using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FYP_Sharebits.Interfaces
{
    public interface IStepCounter
    {
        int Steps { get; set; }

        bool IsAvailable();

        void InitSensorService();

        void StopSensorService();
    }
    
}
