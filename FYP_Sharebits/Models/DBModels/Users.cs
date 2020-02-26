﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class Users
    {
        public String userID { get; set; }

        public String userName { get; set; }

        public Decimal height { get; set; }

        public Decimal weight { get; set; }

        public DateTime birthday { get; set; }

        public String gender { get; set; }

        public String sessionToken { get; set; }
    }
}
