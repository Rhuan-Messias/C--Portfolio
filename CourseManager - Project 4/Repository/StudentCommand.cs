using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Dapper;

namespace CourseManager.Models
{
    internal class StudentCommand
    {
        private string _connectionString;

        public StudentCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<StudentModel> GetList()
        {
            List<StudentModel> students = new List<StudentModel>();

            var sql = "Student_GetList";

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                students = connection.Query<StudentModel>(sql).ToList();
            }

                return students;
        }
    }
}
