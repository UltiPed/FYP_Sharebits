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
            //*/
            //////////////////////////////////////////////////

            database.CreateTableAsync<Users>().Wait();
            database.CreateTableAsync<HabitPlans>().Wait();
            database.CreateTableAsync<PlanItems>().Wait();
            database.CreateTableAsync<PlanRecords>().Wait();
            

        }

        public Task<List<Users>> GetUsersAsync()
        {
            return database.Table<Users>().ToListAsync();
        }

        public Task<List<HabitPlans>> GetPlansAsync()
        {
            return database.Table<HabitPlans>().ToListAsync();
        }

        public Task<List<PlanItems>> GetItemsAsync()
        {
            return database.Table<PlanItems>().ToListAsync();
        }
        /*
        public Task<List<Users>> QueryTableAsync(String queryString)
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

        public Task<int> UpdateRow<T>(T aRow)
        {
            return database.UpdateAsync(aRow);
        }
    }
}
