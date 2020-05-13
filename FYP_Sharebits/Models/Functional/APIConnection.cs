using FYP_Sharebits.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GraphQL.Client.Http;
using GraphQL;
using Xamarin.Essentials;

namespace FYP_Sharebits.Models.Functional
{
    class APIConnection
    {
        public static HttpClient client;
        public static String GraphQLURL = "https://fyp-graphql.herokuapp.com/graphql";

        public static async Task<HabitPlan> GetAllPlans_GraphQL(String queryString)
        {
            var GraphQLClient = new GraphQLHttpClient(GraphQLURL);

            GraphQLRequest graphQLRequest = new GraphQLRequest
            {
                Query = @"
                        HabitPlan {
                            _id,
                            habitName,
                            habitType,
                            startDate,
                            endDate,
                            createdItems{
                                _id,
                                itemType,
                                itemGoal,
                                createdRecords{
                                    recordDate,
                                    progress,
                                    isDone
                                }
                            },
                            creator{
                                userName,
                                password,
                                email,
                                height,
                                weight
                            }
                        }"

            };

            var graphQLResponse = await GraphQLClient.SendQueryAsync<HabitPlan>(graphQLRequest);

            var result = graphQLResponse.Data;

            return result;
        }

        public static async Task<Testing> GetAllPlans()
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1ZTM2OGJlNjE3MGMzMTBkZTk3NDMzMzAiLCJlbWFpbCI6InRlc3RAdGVzdC5jb20iLCJpYXQiOjE1ODM5OTgyODV9._14-GU37DRB2cI0-2FsC_EfpMVt2ufIH6NVl3RSfLI8");

            StringContent stringContent = new StringContent("{\"query\": \"query{ habitPlan { _id, habitName habitType, startDate, endDate, createdItems{ _id, itemType, itemGoal, createdRecords{ recordDate, progress, isDone } }, creator{ userName, password, email, height, weight }        }    }\"}", System.Text.Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);

            var json = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Testing>(json);

            return result;
        }

        public static async Task<BaseModel> Login(String email, String password)
        {
            client = new HttpClient();
            //{\"query\":\"query{login(email: \"{0}\", password: \"{1}\"){token, userId}}\"}
            String query = String.Format("{{\"query\":\"query{{login(email: \\\"{0}\\\", password: \\\"{1}\\\"){{token, userId}}}}\"}}", email, password);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> Logout()
        {
            client = new HttpClient();
            AuthData authData = await Constants.GetAuthData();
            String query = String.Format("{{\"query\":\"query{{logout(userId: \\\"{0}\\\", token: \\\"{1}\\\"){{message}}}}\"}}", authData.UserId, authData.Token);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async  Task<BaseModel> Signup(User user)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{createUser(userinput: {{ userName: \\\"{0}\\\", email: \\\"{1}\\\", password: \\\"{2}\\\", height: \\\"{3}\\\", weight: \\\"{4}\\\", gender: \\\"{4}\\\" }}){{ _id }}}}\"}}", user.UserName, user.Email, user.Password, user.Height, user.Weight, user.Gender);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }
    }
}
