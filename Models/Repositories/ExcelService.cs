using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.Cells;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.AlumniService;

namespace AlumniManagement.Models.Repositories
{
    public class ExcelService : IExcelService
    {
        private AlumniServicesClient _alumniServiceClient;

        public ExcelService()
        {
            _alumniServiceClient = new AlumniServicesClient();
        }

        public Workbook AlumniExportToExcel()
        {
            var data = _alumniServiceClient.GetAlumnis().Select(p => new
            {
                AlumniID = p.AlumniID,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                LastName = p.LastName,
                Email = p.Email,
                MobileNumber = p.MobileNumber,
                Address = p.Address,               
                StateName = p.StateName,                
                DistrictName = p.DistrictName,
                GraduationYear = p.GraduationYear,
                Degree = p.Degree,
                FacultyName = p.FacultyName,
                MajorName = p.MajorName,
                LinkedInProfile = p.LinkedInProfile,
                StateID = p.StateID,
                DistrictID = p.DistrictID,
            });

            //dropdown
            var states = data.Select(s => s.StateName).ToList();
            var districts = data.Select(d => d.DistrictName).ToList();

            //new Workbook
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];

            int[] maxColumnWidths = new int[13]; //13 exclude AlumniID, StateID, DistrictID

            worksheet.Name = "Alumni Data";

            //Add Header
            worksheet.Cells[0, 0].PutValue("AlumniID");
            worksheet.Cells[0, 1].PutValue("First Name");
            worksheet.Cells[0, 2].PutValue("Middle Name");
            worksheet.Cells[0, 3].PutValue("Last Name");
            worksheet.Cells[0, 4].PutValue("Email");
            worksheet.Cells[0, 5].PutValue("Mobile Number");
            worksheet.Cells[0, 6].PutValue("Address");
            worksheet.Cells[0, 7].PutValue("State");
            worksheet.Cells[0, 8].PutValue("District");
            worksheet.Cells[0, 9].PutValue("Graduation Year");
            worksheet.Cells[0, 10].PutValue("Degree");
            worksheet.Cells[0, 11].PutValue("Faculty");
            worksheet.Cells[0, 12].PutValue("Major");
            worksheet.Cells[0, 13].PutValue("LinkedIn Profile");

            //Set for customize column width
            maxColumnWidths[1] = "First Name".Length;
            maxColumnWidths[2] = "Middle Name".Length;
            maxColumnWidths[3] = "Last Name".Length;
            maxColumnWidths[4] = "Email".Length;
            maxColumnWidths[5] = "Mobile Number".Length;
            maxColumnWidths[6] = "Address".Length;
            maxColumnWidths[7] = "State".Length;
            maxColumnWidths[8] = "District".Length;
            maxColumnWidths[9] = "Graduation Year".Length;
            maxColumnWidths[10] = "Degree".Length;
            maxColumnWidths[11] = "Faculty".Length;
            maxColumnWidths[12] = "Major".Length;
            maxColumnWidths[13] = "LinkedIn Profile".Length;

            //set hide AlumniID, StateID, DistrictID
            worksheet.Cells.HideColumns(0, 0); //hide AlumniID
            worksheet.Cells.HideColumns(0, 14); //hide StateID
            worksheet.Cells.HideColumns(0, 15); //hide DistrictID

            //add Lock Style
            Style unlockedStyle = worksheet.Workbook.CreateStyle();
            unlockedStyle.HorizontalAlignment = TextAlignmentType.Center;
            unlockedStyle.IsLocked = false;
            worksheet.Cells.ApplyStyle(unlockedStyle, new StyleFlag() { Locked = true });

            Style style = worksheet.Workbook.CreateStyle();
            StyleFlag styleFlag = new StyleFlag();
            style.IsLocked = true;
            styleFlag.Locked = true;
            worksheet.Cells.Columns[0].ApplyStyle(style, styleFlag); //AlumniID
            worksheet.Cells.Columns[14].ApplyStyle(style, styleFlag); //StateID

            worksheet.Protect(ProtectionType.All);

            for (int i = 0; i < data.Count(); i++)
            {
                worksheet.Cells[i]
            }
        }

        //--------------------------------------------------------------------------------

        public void ImportAlumniFromExcelToDatabase(HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }
    }
}