using FYP_Sharebits.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FYP_Sharebits.Models.Functional
{
    public class StepController
    {
        public static readonly String stepPropertyString = "StepsProperty";

        public static String todayString
        {
            get { return DateTime.Today.Date.ToString(); }
        }

        public static String errMsg = "";

        
        public static async Task<int> GetTodaySteps_Properties()
        {
            await CheckStepProperties();

            Dictionary<String, int> tempDict = Application.Current.Properties[stepPropertyString] as Dictionary<String, int>;
            if (!tempDict.ContainsKey(todayString))
            {
                return 0;
            } else
            {
                return tempDict[todayString];
            }
        }

        public static async Task UpdateTodaySteps_Properties()
        {
            var checkProp = await CheckStepProperties();

            if (checkProp)
            {
                if (!(Application.Current.Properties[stepPropertyString] as Dictionary<String, int>).ContainsKey(todayString))
                {
                    (Application.Current.Properties[stepPropertyString] as Dictionary<String, int>).Add(todayString, 0);
                    await Application.Current.SavePropertiesAsync();
                }

                (Application.Current.Properties[stepPropertyString] as Dictionary<String, int>)[todayString] += 1;
                await Application.Current.SavePropertiesAsync();
            }
        }
        
        public static async Task<int> GetTodaySteps_DB()
        {
            //return await await GetTodaySteps_Properties();

            return await App.Database.GetTodayStepCount();

        }

        public static async Task UpdateTodaySteps_DB()
        {
            var result = await App.Database.UpdateStepRecord();
            if (!result)
            {
                errMsg = "Error in updating Step counts in DB";
            }
        }

        public static async Task<Boolean> CheckStepProperties()
        {
            if (!Application.Current.Properties.ContainsKey(stepPropertyString))
            {
                Dictionary<String, int> stepDict = new Dictionary<string, int>();
                Application.Current.Properties.Add(stepPropertyString, stepDict);
                await Application.Current.SavePropertiesAsync();
            }

            return true;
        }
    }
}
