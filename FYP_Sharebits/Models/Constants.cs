using FYP_Sharebits.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FYP_Sharebits.Models
{
    public static class Constants
    {
        public static AuthData authData= null;

        public static Boolean CheckAuth()
        {
            if (String.IsNullOrEmpty(authData.UserId)|| String.IsNullOrEmpty(authData.Token))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}