using CourseManager.Models;
using Dapper;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CourseManager.Repository
{
    internal class EnrollmentCommand
    {
        private string _connectionString;

        //Constructor
        public EnrollmentCommand(string connectionString)
        {
            this._connectionString = connectionString;
        }

        //GetList Method
        public IList<EnrollmentModel> GetList()
        {
            List<EnrollmentModel> enrollments = new List<EnrollmentModel>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                enrollments = connection.Query<EnrollmentModel>("Enrollments_GetList").ToList();
            }
            ;

            foreach (var enrollment in enrollments)
            {
                enrollment.IsCommited = true;
            }
            
            return enrollments;
        }

        //Upsert Method
        public void Upsert(EnrollmentModel enrollmentModel) //it receives a model to be upserted
        {
            var sql = "Enrollments_Upsert";
            //Below we get the current windows user. We could just ask the user to input a name
            var userId = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

            //Creating the datatable to be sent
            var dataTable = new DataTable();
            dataTable.Columns.Add("EnrollmentId",typeof(int));
            dataTable.Columns.Add("StudentId", typeof(int));
            dataTable.Columns.Add("CourseId", typeof(int));
            dataTable.Rows.Add(enrollmentModel.EnrollmentId,
                               enrollmentModel.StudentId,
                               enrollmentModel.CourseId);

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                /*First we create a table and the userId, that is required in the Stored Procedure, after that
                 we user the Execute Dapper method and pass the Sql Stored Procedure, and new row passing the 
                 arguments as created in the database, the enrollment type we use the method AsTableValuedParameter
                 to indicate whats the table type we created in the Sql database for this upsert, and in the 
                 command type, we indicate that it is a stored procedure in the sql database*/
                connection.Execute(sql, new {@EnrollmentType = dataTable.AsTableValuedParameter("EnrollmentType"),
                                             @UserId = userId},
                                             commandType: CommandType.StoredProcedure);
            }


        }


    }
}
