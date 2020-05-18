using FYP_Sharebits.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FYP_Sharebits.Views.Social
{
    public class TempData
    {
        public static ObservableCollection<HabitPlans> plans;

        public static ObservableCollection<PlanItems> planItems;

        public static ObservableCollection<Users> users;

        public static void createDummyPlans()
        {
            plans = new ObservableCollection<HabitPlans>();

            HabitPlans plan = new HabitPlans();
            plan.habitID = 1;
            plan.habitName = "Basic running training";
            plan.habitType = "Normal";

            plans.Add(plan);

            HabitPlans plan2 = new HabitPlans();
            plan.habitID = 2;
            plan.habitName = "Walking everyday - Basic";
            plan.habitType = "Normal";

            plans.Add(plan2);

            planItems = new ObservableCollection<PlanItems>();

            PlanItems item = new PlanItems();
            item.habitID = 1;
            item.itemName = "Run for 120 minutes per day";
            item.itemType = "Duration";
            item.itemGoal = 120;
            planItems.Add(item);

            PlanItems item2 = new PlanItems();
            item2.habitID = 2;
            item2.itemName = "Walk for 120 minutes per day";
            item2.itemType = "Duration";
            item2.itemGoal = 120;
            planItems.Add(item2);

            PlanItems item3 = new PlanItems();
            item3.habitID = 2;
            item3.itemName = "Walk 2000 meter per day";
            item3.itemType = "Distance";
            item3.itemGoal = 2000;
            planItems.Add(item3);

            users = new ObservableCollection<Users>();
            Users user = new Users();
            user.birthday = new DateTime(1995, 5, 5);
            user.userID = "DemoUser01";
            user.userName = "01DemoUser";
            user.gender = "M";
            user.height = 165;
            user.weight = 70;
            users.Add(user);

            Users user2 = new Users();
            user2.birthday = new DateTime(1999, 7, 1);
            user2.userID = "DemoUser02";
            user2.userName = "02DemoUser";
            user2.gender = "F";
            user2.height = 161;
            user2.weight = 45;
            users.Add(user2);
        }

        public static ObservableCollection<PlanItems> GetPlanItems(int habitID)
        {
            return new ObservableCollection<PlanItems>(planItems.Where(x => x.habitID == habitID));
        }

        public static Users GetUser(int habitID)
        {
            return users.Where(x => x.userID == ("DemoUser0" + habitID)).FirstOrDefault();
        }
    }
}
