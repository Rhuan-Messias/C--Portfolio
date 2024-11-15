using CustomerOrderViewer2.Models;
using Dapper;
using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderViewer2.Repository
{
    class CustomerCommand
    {
        private string _connectionString;

        public CustomerCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<CustomerModel> GetList()
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                customers = connection.Query<CustomerModel>("[CustomerOrderViewer].[dbo].Customer_GetList").ToList();
            }

                return customers;
        }
    }
}
