﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using FYP_Sharebits.Models.DBModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FYP_Sharebits.Data
{
    public class SharebitsDB
    {
        readonly SQLiteAsyncConnection database;

        public SharebitsDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            //Do it if and only if you have built these table before and you now have some changes to any model of them
            //////////////////////////////////////////////////
            /*
            database.DropTableAsync<Users>().Wait();
            database.DropTableAsync<HabitPlans>().Wait();
            database.DropTableAsync<PlanItems>().Wait();
            database.DropTableAsync<PlanRecords>().Wait();
            database.DropTableAsync<StepCounts>().Wait();
            */
            //////////////////////////////////////////////////

            database.CreateTableAsync<Users>().Wait();
            database.CreateTableAsync<HabitPlans>().Wait();
            database.CreateTableAsync<PlanItems>().Wait();
            database.CreateTableAsync<PlanRecords>().Wait();
            database.CreateTableAsync<StepCounts>().Wait();

            //For demo only
            //////////////////////////////////////////////////
            /*
            database.DropTableAsync<Coachs>().Wait();
            database.DropTableAsync<CoachPlans>().Wait();
            database.DropTableAsync<CoachPlanItems>().Wait();
            database.DropTableAsync<Students>().Wait();
            database.DropTableAsync<CoachingRequest>().Wait();
            */
            
            /*
            database.CreateTableAsync<Coachs>().Wait();
            database.CreateTableAsync<CoachPlans>().Wait();
            database.CreateTableAsync<CoachPlanItems>().Wait();
            database.CreateTableAsync<Students>().Wait();
            database.CreateTableAsync<CoachingRequest>().Wait();

            //Just execute once only for every time you create tables first time or after dropping them for demo
            //InsertDemoCoach().Wait();

            */
            //////////////////////////////////////////////////

            //Do it if you need a demo user
            //////////////////////////////////////////////////

            //InsertDemoUser().Wait();

            //
            //////////////////////////////////////////////////


        }

        public Task<List<Users>> GetUsersAsync()
        {
            return database.Table<Users>().ToListAsync();
        }

        public Task<List<HabitPlans>> GetPlansAsync()
        {
            return database.Table<HabitPlans>().ToListAsync();
        }
        /*
        public Task<List<CoachPlans>> GetCoachPlansAsync()
        {
            return database.Table<CoachPlans>().ToListAsync();
        }

        public Task<List<Coachs>> GetCoachs()
        {
            return database.Table<Coachs>().ToListAsync();
        }
        */

        public Task<List<PlanItems>> GetItemsAsync()
        {
            return database.Table<PlanItems>().ToListAsync();
        }

        public Task<List<PlanRecords>> GetRecordsAsync()
        {
            return database.Table<PlanRecords>().ToListAsync();
        }

        /*
        public Task<List<Users>> QueryTaAsync(String queryString)
        {
            return database.QueryAsync(;
        }
        /*
        public Task<int> UpdateUsers(Users user)
        {
            return database.UpdateAsync<Users>()
        }
        */
        public Task<int> InsertRow<T>(T anObject)
        {
            return database.InsertAsync(anObject);
        }

        public Task<List<PlanItems>> QueryPlanItems(String QueryString)
        {
            return database.QueryAsync<PlanItems>(QueryString);
        }

        public Task<List<HabitPlans>> QueryHabitPlans(String QueryString)
        {
            return database.QueryAsync<HabitPlans>(QueryString);
        }
        /*
        public Task<List<Coachs>> QueryCoachs(String QueryString)
        {
            return database.QueryAsync<Coachs>(QueryString);
        }

        public Task<List<Students>> QueryStudents(String QueryString)
        {
            return database.QueryAsync<Students>(QueryString);
        }

        public Task<List<CoachingRequest>> QueryRequests(String QueryString)
        {
            return database.QueryAsync<CoachingRequest>(QueryString);
        }
        
        public Task<List<CoachPlans>> QueryCoachPlans(String QueryString)
        {
            return database.QueryAsync<CoachPlans>(QueryString);
        }

        public Task<List<CoachPlanItems>> QueryCoachPlanItems(String QueryString)
        {
            return database.QueryAsync<CoachPlanItems>(QueryString);
        }
        */
        public Task<List<PlanRecords>> QueryPlanRecords(String QueryString, int itemID, DateTime todayDate)
        {
            return database.QueryAsync<PlanRecords>(QueryString, itemID, todayDate);
        }

        public Task<List<PlanRecords>> QueryPlanRecords(String QueryString, DateTime todayDate)
        {
            return database.QueryAsync<PlanRecords>(QueryString, todayDate);
        }

        public Task<List<PlanRecords>> QueryPlanRecords(String QueryString)
        {
            return database.QueryAsync<PlanRecords>(QueryString);
        }

        /*
        public Task<int> DeleteUserAsync(Users user)
        {
            return database.DeleteAsync(user);
        }
        */

        public Task<int> ExecuteQuery(String queryString)
        {
            return database.ExecuteAsync(queryString);
        }
        
        public Task<int> UpdateRow<T>(T aRow)
        {
            return database.UpdateAsync(aRow);
        }

        public async Task<Boolean> SetSessionToken(String tokenString)
        {
            var users = (await database.QueryAsync<Users>("SELECT * FROM Users"));
            if (users.Count == 0)
            {
                return false;
            }

            var user = users[0];

            user.sessionToken = tokenString;
            var result = await UpdateRow(user);

            if (result > 0)
            {
                //Success...
                return true;
            } else
            {
                //Fail...
                return false;
            }
        }

        public async Task<Boolean> UpdateStepRecord()
        {
            int result;

            //String queryString = "Update StepCounts SET stepCount=stepCount+1 WHERE recordDate = ?";

            String queryString2 = "SELECT * FROM [PlanItems] WHERE [itemType]='Steps'";
            var result2 = await QueryPlanItems(queryString2);

            String itemIDs = "(";
            Boolean isFirst = true;
            foreach (PlanItems item in result2)
            {
                if (isFirst)
                {
                    itemIDs = itemIDs + item.itemID;
                    isFirst = false;
                }
                else
                {
                    itemIDs = itemIDs + ", " + item.itemID;
                }
            }
            itemIDs = itemIDs + ")";

            String queryString3 = "Update [PlanRecords] SET progress=progress+1 WHERE recordDate = ? AND itemID IN " + itemIDs;

            result = await database.ExecuteAsync(queryString3, DateTime.Today.Date);

            /*
            var StepCounts = await database.Table<StepCounts>().ToListAsync();
            var TodayCount = StepCounts.Find(x => x.recordDate == DateTime.Today.Date);

            if (TodayCount == null)
            {
                TodayCount = new StepCounts();
                TodayCount.recordDate = DateTime.Today.Date;
                TodayCount.stepCount = 1;
                result = await InsertRow(TodayCount);
            }
            else
            {
                result = await database.ExecuteAsync(queryString, DateTime.Today.Date);
            }
            */

            if (result > 0)
            {
                return true;
            } else
            {
                return false;
            }

        }

        public async Task<int> GetTodayStepCount()
        {
            var Counts = await database.Table<StepCounts>().ToListAsync();
            var todayCounts = Counts.Find(x => x.recordDate == DateTime.Today.Date);

            if (todayCounts != null)
            {
                return todayCounts.stepCount;
            }
            else
            {
                return 0;
            }
            

        }
    }
}
