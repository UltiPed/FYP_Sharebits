using FYP_Sharebits.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Essentials;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace FYP_Sharebits.Models
{
    public static class Constants
    {
        public async static Task<Boolean> IsAuth()
        {
            AuthData authData;
            try
            {
                authData = await GetAuthData();
            }
            catch(Exception)
            {
                return false;
            }
            
            if (String.IsNullOrEmpty(authData.UserId) || String.IsNullOrEmpty(authData.Token))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async static Task<AuthData> GetAuthData()
        {
            AuthData authData = new AuthData()
            {
                UserId = "",
                Token = ""
            };
            try
            {
                authData.UserId = await SecureStorage.GetAsync("UserId");
                authData.Token = await SecureStorage.GetAsync("Token");
                return authData;
            }
            catch (Exception)
            {
                return authData;
            }
        }

        public async static Task<String> GetToken()
        {
            String token = String.Empty;
            try
            {
                token = await SecureStorage.GetAsync("Token");
                return token;
            } catch(Exception)
            {
                return token;
            }
        }

        public async static Task<String> GetUserId()
        {
            String userId = String.Empty;
            try
            {
                userId = await SecureStorage.GetAsync("UserId");
                return userId;
            }
            catch (Exception)
            {
                return userId;
            }
        }
    }
}