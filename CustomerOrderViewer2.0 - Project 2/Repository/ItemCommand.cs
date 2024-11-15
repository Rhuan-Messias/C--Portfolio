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
    class ItemCommand
    {
        private string _connectionString;
        
        public ItemCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<ItemModel> GetList()
        {
            List<ItemModel> items = new List<ItemModel>();

            var sql = "[CustomerOrderViewer].[dbo].Item_GetList";//Here we'll use the SPROC's created on SQL

            //using will keep this working only while needed
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                /*with the Query from dapper we can pass the procedure that will get
                 the data and convert into the model*/
                items = connection.Query<ItemModel>(sql).ToList();
            }

            return items;
        }
    }
}
