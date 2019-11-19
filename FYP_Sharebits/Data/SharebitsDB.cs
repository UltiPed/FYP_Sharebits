using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using FYP_Sharebits.Models.DBModels;
using System.Threading.Tasks;

namespace FYP_Sharebits.Data
{
    public class SharebitsDB
    {
        readonly SQLiteAsyncConnection database;

        public SharebitsDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Users>().Wait();
            database.CreateTableAsync<HabitPlans>().Wait();
            database.CreateTableAsync<PlanItems>().Wait();
            database.CreateTableAsync<PlanRecords>().Wait();
        }

        public Task<List<Users>> GetUsersAsync()
        {
            return database.Table<Users>().ToListAsync();
        }

        public Task<List<Users>> QueryTableAsync(String queryString)
        {
            return database.QueryAsync<Users>(queryString);
        }

        public Task<int> UpdateUsers(Users user)
        {
            return database.UpdateAsync(user);
        }
        
        public Task<int> InsertUser(Users user)
        {
            return database.InsertAsync(user);
        }

        public Task<int> DeleteUserAsync(Users user)
        {
            return database.DeleteAsync(user);
        }
    }
}
