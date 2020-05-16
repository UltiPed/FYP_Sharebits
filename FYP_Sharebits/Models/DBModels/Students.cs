using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class Students
    {
        public int coachID { get; set; }

        public String coachName { get; set; }

        public String studentID { get; set; }

        public String studentName { get; set; }
    }
}
