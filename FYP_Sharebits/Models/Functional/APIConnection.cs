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
using System.Collections.ObjectModel;
using FYP_Sharebits.Models.DBModels;
using Xamarin.Forms;
using System.Linq;

namespace FYP_Sharebits.Models.Functional
{
    class APIConnection
    {
        public static HttpClient client;
        public static String GraphQLURL = "https://fyp-graphql.herokuapp.com/graphql";

        private static String authenticationCode = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1ZTM2OGJlNjE3MGMzMTBkZTk3NDMzMzAiLCJlbWFpbCI6InRlc3RAdGVzdC5jb20iLCJpYXQiOjE1ODM5OTgyODV9._14-GU37DRB2cI0-2FsC_EfpMVt2ufIH6NVl3RSfLI8";

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

        /*
        public static async Task<Testing> GetAllPlans()
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", authenticationCode);

            StringContent stringContent = new StringContent("{\"query\": \"query{ habitPlan { _id, habitName habitType, startDate, endDate, createdItems{ _id, itemType, itemGoal, createdRecords{ recordDate, progress, isDone } }, creator{ userName, password, email, height, weight }        }    }\"}", System.Text.Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);

            var json = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Testing>(json);

            return result;
        }
        */

        public static async Task<BaseModel> Login(String email, String password)
        {
            client = new HttpClient();
            //{\"query\":\"query{login(email: \"{0}\", password: \"{1}\"){token, userId}}\"}
            String query = String.Format("{{\"query\":\"query{{login(email: \\\"{0}\\\", password: \\\"{1}\\\"){{token, userId, userName}}}}\"}}", email, password);
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

        public static async Task<BaseModel> Signup(User user)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{createUser(userInput: {{ userName: \\\"{0}\\\", email: \\\"{1}\\\", password: \\\"{2}\\\", height: {3}, weight: {4}, gender: \\\"{5}\\\" }}){{ _id }}}}\"}}", user.UserName, user.Email, user.Password, user.Height, user.Weight, user.Gender);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> searchPlan(String keywords)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"query{{ searchPlan(keyword: \\\"{0}\\\"){{ _id,  habitName,  habitType, creator{{ _id, email, userName, height, weight, gender}}, createdItems{{itemName, itemType, itemGoal}}}}}}\"}}", keywords);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> findCoaches()
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"query{{findCoaches{{_id, user{{ _id, email, userName, height, weight, gender}}}}}}\"}}");
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> beCoach(String userID)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{ beCoach(userId: \\\"{0}\\\") {{ message }} }}\"}}", userID);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> PushPlans(ObservableCollection<HabitPlans> plans, ObservableCollection<PlanItems> items, String userID)
        {
            String queryPart = PrepareQuery_pushPlans(plans, items, userID);
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{ {0}  {{ _id, localID }} }}\"}}", queryPart);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static String PrepareQuery_pushPlans(ObservableCollection<HabitPlans> plans, ObservableCollection<PlanItems> items, String userID)
        {
            String returnQuery = "pushPlans(creator: \\\"" + userID + "\\\", newPlans: [";
            Boolean isFirst = true;

            foreach(HabitPlans plan in plans)
            {
                if (!isFirst)
                {
                    returnQuery += ", ";
                }
                returnQuery += "{habitName: \\\"" + plan.habitName + "\\\", ";
                returnQuery += "habitType: \\\"" + plan.habitType + "\\\", ";

                DateTime startTime = plan.startDate;
                startTime = DateTime.SpecifyKind(startTime, DateTimeKind.Local);
                DateTimeOffset startTime2 = startTime;
                returnQuery += "startDate: \\\"" + startTime2.ToString() + "\\\",";

                if (plan.habitType.Equals("Challenge"))
                {
                    DateTime endTime = plan.endDate;
                    startTime = DateTime.SpecifyKind(startTime, DateTimeKind.Local);
                    DateTimeOffset endTime2 = endTime;
                    returnQuery += "endDate: \\\"" + endTime2.ToString() + "\\\",";
                }
                returnQuery += "localID: " + plan.habitID + ",";

                var items2 = items.Where(x => x.habitID == plan.habitID);
                returnQuery += "Items: [";

                Boolean isFirst2 = true;
                foreach(PlanItems item in items2)
                {
                    if (!isFirst2)
                    {
                        returnQuery += ", ";
                    }
                    returnQuery += "{itemName: \\\"" + item.itemName + "\\\", ";
                    returnQuery += "itemType: \\\"" + item.itemType + "\\\", ";
                    returnQuery += "itemGoal: " + item.itemGoal + ", ";
                    returnQuery += "localID: " + plan.habitID + "}";
                    isFirst2 = false;
                }

                returnQuery += "]}";
                isFirst = false;
            }

            returnQuery += "])";
            return returnQuery;
        }

        public static async Task<BaseModel> getStudents(String CoachID)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"query{{getStudents(coachId: \\\"{0}\\\"){{_id, user{{ _id, email, userName, height, weight, gender}}}}}}\"}}", CoachID);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> getCoaches(String userId)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"query{{getCoaches(userId: \\\"{0}\\\"){{ Coach{{ _id, user {{email, userName, height, weight, gender}} }} }} }}\"}}", userId);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> getCoachingReq(String CoachID)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"query{{getCoachingReq(coachId: \\\"{0}\\\"){{_id, user{{ _id, email, userName, height, weight, gender}}}}}}\"}}", CoachID);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> getComment(String planID)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"query{{getComment(planId: \\\"{0}\\\"){{user{{ _id, email, userName, height, weight, gender}}, content, recordDate}}}}\"}}", planID);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> pullPlans(String userID)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"query{{ pullPlans(userID: \\\"{0}\\\"){{ _id,  habitName,  habitType, startDate, endDate, createdItems{{itemName, itemType, itemGoal}}}}}}\"}}", userID);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> setPublish(String planID, String isPublish)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{ setPublish(plan_id: \\\"{0}\\\", isPublish: \\\"{1}\\\") {{ message }} }}\"}}", planID, isPublish);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> addCoach(String userID, String CoachID)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{ addCoach(userId: \\\"{0}\\\", coachId: \\\"{1}\\\") {{ message }} }}\"}}", userID, CoachID);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> createCoachingReq(String userID, String CoachID)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{ createCoachingReq(userId: \\\"{0}\\\", coachId: \\\"{1}\\\") {{ message }} }}\"}}", userID, CoachID);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> delCoachingReq(String reqId)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{ delCoachingReq(reqId: \\\"{0}\\\") {{ message }} }}\"}}", reqId);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }

        public static async Task<BaseModel> commentPlan(String userID, String planID, String commentTime, String content)
        {
            client = new HttpClient();
            String query = String.Format("{{\"query\":\"mutation{{ commentPlan(planId: \\\"{0}\\\", userId: \\\"{1}\\\", createDate: \\\"{2}\\\", content: \\\"{3}\\\") {{ message }} }}\"}}", planID, userID, commentTime, content);
            StringContent stringContent = new StringContent(query, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseModel>(json);
            return result;
        }
    }
}
