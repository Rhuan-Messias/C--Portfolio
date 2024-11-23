using CourseManager.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CourseManager.Repository
{
    internal class CourseCommand
    {
        private string _connectionString;

        public CourseCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<CourseModel> GetList()
        {
            List<CourseModel> courses = new List<CourseModel>();

            var sql = "Course_GetList";

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                //It says to run the sql stored procedure and put the result as a List inside my courses list
                courses = connection.Query<CourseModel>(sql).ToList();
            }

            return courses;
        }

    }
}
