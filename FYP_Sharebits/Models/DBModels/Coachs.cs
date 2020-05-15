using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class Coachs
    {
        [PrimaryKey, AutoIncrement]
        public int coachID { get; set; }

        public String userID { get; set; }

        public String password { get; set; }

        public String userName { get; set; }

        public Decimal height { get; set; }

        public Decimal weight { get; set; }

        public DateTime birthday { get; set; }

        public String gender { get; set; }

        public Coachs() { }
    }
}
