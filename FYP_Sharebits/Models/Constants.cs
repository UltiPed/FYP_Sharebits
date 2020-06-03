using FYP_Sharebits.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Essentials;

using Xamarin.Forms;
using System.Threading.Tasks;
using FYP_Sharebits.Models.Functional;
using System.Collections.ObjectModel;

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
                UserName = "",
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

        public async static Task<String> GetUserName()
        {
            String userName = String.Empty;
            try
            {
                userName = await SecureStorage.GetAsync("UserName");
                return userName;
            }
            catch (Exception)
            {
                return userName;
            }
        }

        public async static Task<String> GetCoachID()
        {
            String result = String.Empty;
            String userID = await GetUserId();

            result = await SecureStorage.GetAsync("CoachID");

            if (result == null)
            {

                var findCoach = await APIConnection.findCoaches();
                if (findCoach.Errors == null)
                {
                    var coachs = new ObservableCollection<Coach>(findCoach.Data.FindCoaches);
                    var targetCoach = coachs.Where(x => x.User.Id.Equals(userID)).FirstOrDefault();
                    if (targetCoach != null)
                    {
                        result = targetCoach.Id;
                        await SecureStorage.SetAsync("CoachID", result);
                    }
                }
            }

            return result;
        }

        public async static Task<Boolean> CheckCoach()
        {
            String coachID = await GetCoachID();
            if (String.IsNullOrEmpty(coachID))
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}