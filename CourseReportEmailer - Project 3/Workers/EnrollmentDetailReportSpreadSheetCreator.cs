using CourseReportEmailer.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CourseReportEmailer.Workers
{
    internal class EnrollmentDetailReportSpreadSheetCreator
    {
        public void Create(string fileName, IList<EnrollmentDetailReportModel> enrollmentModels)
        {
            using (var document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                var json = JsonConvert.SerializeObject(enrollmentModels);
                DataTable enrollmentsTable = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheetList = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                Sheet singleSheet = new Sheet()
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Report Sheet"
                };

                sheetList.Append(singleSheet);            //Column1 Column2 Column3 
                                                          //Data1   Data1   Data1
                Row excelTitleRow = new Row();            //Data2   Data2   Data2

                foreach (DataColumn tableColumn in enrollmentsTable.Columns)
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(tableColumn.ColumnName);
                    excelTitleRow.Append(cell);
                }

                sheetData.AppendChild(excelTitleRow);

                foreach (DataRow tableRow in enrollmentsTable.Rows)
                {
                    Row excelNewRow = new Row();
                    foreach (DataColumn tableColumn in enrollmentsTable.Columns)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(tableRow[tableColumn.ColumnName].ToString());
                        excelNewRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(excelNewRow);
                }

                workbookPart.Workbook.Save();
            }
        }
    }
}
