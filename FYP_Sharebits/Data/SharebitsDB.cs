using SQLite;
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
            ///*
            database.CreateTableAsync<Coachs>().Wait();
            database.CreateTableAsync<CoachPlans>().Wait();
            database.CreateTableAsync<CoachPlanItems>().Wait();
            database.CreateTableAsync<Students>().Wait();
            database.CreateTableAsync<CoachingRequest>().Wait();

            //Just execute once only for every time you create tables first time or after dropping them for demo
            //InsertDemoCoach().Wait();

            //*/
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

        public Task<List<CoachPlans>> GetCoachPlansAsync()
        {
            return database.Table<CoachPlans>().ToListAsync();
        }

        public Task<List<Coachs>> GetCoachs()
        {
            return database.Table<Coachs>().ToListAsync();
        }

        public Task<List<PlanItems>> GetItemsAsync()
        {
            return database.Table<PlanItems>().ToListAsync();
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

        public Task<List<PlanRecords>> QueryPlanRecords(String QueryString, int itemID, DateTime todayDate)
        {
            return database.QueryAsync<PlanRecords>(QueryString, itemID, todayDate);
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

        public Task<int> InsertDemoUser()
        {
            Users user = new Users();
            user.birthday = new DateTime(1989, 6, 4);
            user.userID = "DemoUser01";
            user.userName = "01DemoUser";
            user.gender = "M";
            user.height = 165;
            user.weight = 55;
            user.sessionToken = "";

            return InsertRow(user);
        }

        public Task<int> InsertDemoCoach()
        {
            Coachs coach = new Coachs();
            coach.birthday = new DateTime(1989, 6, 4);
            coach.userID = "test@test.com";
            coach.password = "tester";
            coach.userName = "democoach_1";
            coach.gender = "M";
            coach.height = 165;
            coach.weight = 55;

            InsertRow(coach);

            Coachs coach2 = new Coachs();
            coach2.birthday = new DateTime(1989, 6, 4);
            coach2.userID = "test2@test.com";
            coach2.password = "tester";
            coach2.userName = "democoach_2";
            coach2.gender = "M";
            coach2.height = 165;
            coach2.weight = 55;

            InsertRow(coach2);

            Coachs coach3 = new Coachs();
            coach3.birthday = new DateTime(1989, 6, 4);
            coach3.userID = "5e38108179c493001751aabb";
            coach3.password = "tester";
            coach3.userName = "democoach_3";
            coach3.gender = "M";
            coach3.height = 165;
            coach3.weight = 55;

            return InsertRow(coach3);
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

            String queryString = "Update StepCounts SET stepCount=stepCount+1 WHERE recordDate = ?";

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
