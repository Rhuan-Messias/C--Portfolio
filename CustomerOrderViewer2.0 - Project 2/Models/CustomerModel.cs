﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CustomerOrderViewer2.Models
{
    class CustomerModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName {  get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
