using FYP_Sharebits.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GraphQL.Client.Http;
using GraphQL;

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

            Array a = new Array[1, 2, 3];
            var a1 = a.ToString();

            List<String> l = new List<string>() { "1", "2", "3" };
            var l1 = l.ToString();

            return result;
        }

        public static async Task AuthTest()
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1ZTM2OGJlNjE3MGMzMTBkZTk3NDMzMzAiLCJlbWFpbCI6InRlc3RAdGVzdC5jb20iLCJpYXQiOjE1ODM5OTgyODV9._14-GU37DRB2cI0-2FsC_EfpMVt2ufIH6NVl3RSfLI8");
            //{""}
            var query = "{ 'query': 'query{ test }' }".Replace("'", "\"");
            
            //"{\"query\": \"query{ test }\"}"
            StringContent stringContent = new StringContent(query, System.Text.Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(GraphQLURL, stringContent);

            var json = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AuthTest>(json);
        }
    }
}
