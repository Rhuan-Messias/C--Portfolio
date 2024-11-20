using CourseReportEmailer.Models;
using Dapper;
using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CourseReportEmailer.Repository
{
    internal class EnrollmentDetailReportCommand
    {
        private string _connectionString;

        public EnrollmentDetailReportCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<EnrollmentDetailReportModel> GetList()
        {
            List<EnrollmentDetailReportModel> enrollmentDetailReport = new List<EnrollmentDetailReportModel>();

            /*What Dapper is going to do is USE CourseReport; EXEC EnrollmentReport_GetList*/
            var sql = "EnrollmentReport_GetList"; //The SPROCs we've created to get the view in sql server

            using(SqlConnection connection = new SqlConnection(this._connectionString))
            {
                enrollmentDetailReport = connection.Query<EnrollmentDetailReportModel>(sql).ToList();
            }
            
            return enrollmentDetailReport;
        }
    }
}
