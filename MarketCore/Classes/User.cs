﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Classes
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birsday { get; set; }
    }
}