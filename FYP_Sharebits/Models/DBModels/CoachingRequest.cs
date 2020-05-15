using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class CoachingRequest
    {
        [PrimaryKey, AutoIncrement]
        public int requestID { get; set; }

        public int coachID { get; set; }

        public String studentID { get; set; }

        public String message { get; set; }
    }
}
