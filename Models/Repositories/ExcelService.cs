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

            int[] maxColumnWidths = new int[14]; //13 exclude AlumniID, StateID, DistrictID

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
                worksheet.Cells[i + 1, 0].PutValue(data.ElementAt(i).AlumniID);
                worksheet.Cells[i + 1, 1].PutValue(data.ElementAt(i).FirstName);
                worksheet.Cells[i + 1, 2].PutValue(data.ElementAt(i).MiddleName);
                worksheet.Cells[i + 1, 3].PutValue(data.ElementAt(i).LastName);
                worksheet.Cells[i + 1, 4].PutValue(data.ElementAt(i).Email);
                worksheet.Cells[i + 1, 5].PutValue(data.ElementAt(i).MobileNumber);
                worksheet.Cells[i + 1, 6].PutValue(data.ElementAt(i).Address);
                worksheet.Cells[i + 1, 7].PutValue(data.ElementAt(i).StateName);
                worksheet.Cells[i + 1, 8].PutValue(data.ElementAt(i).DistrictName);
                worksheet.Cells[i + 1, 9].PutValue(data.ElementAt(i).GraduationYear);
                worksheet.Cells[i + 1, 10].PutValue(data.ElementAt(i).Degree);
                worksheet.Cells[i + 1, 11].PutValue(data.ElementAt(i).FacultyName);
                worksheet.Cells[i + 1, 12].PutValue(data.ElementAt(i).MajorName);
                worksheet.Cells[i + 1, 13].PutValue(data.ElementAt(i).LinkedInProfile);

                maxColumnWidths[1] = Math.Max(maxColumnWidths[1], data.ElementAt(i).FirstName.Length);
                maxColumnWidths[2] = Math.Max(maxColumnWidths[2], data.ElementAt(i).MiddleName.Length);
                maxColumnWidths[3] = Math.Max(maxColumnWidths[3], data.ElementAt(i).LastName.Length);
                maxColumnWidths[4] = Math.Max(maxColumnWidths[4], data.ElementAt(i).Email.Length);
                maxColumnWidths[5] = Math.Max(maxColumnWidths[5], data.ElementAt(i).MobileNumber.Length);
                maxColumnWidths[6] = Math.Max(maxColumnWidths[6], data.ElementAt(i).Address.Length);
                maxColumnWidths[7] = Math.Max(maxColumnWidths[7], data.ElementAt(i).StateName.Length);
                maxColumnWidths[8] = Math.Max(maxColumnWidths[8], data.ElementAt(i).DistrictName.Length);
                maxColumnWidths[9] = Math.Max(maxColumnWidths[9], data.ElementAt(i).GraduationYear.ToString().Length);
                maxColumnWidths[10] = Math.Max(maxColumnWidths[10], data.ElementAt(i).Degree.Length);
                maxColumnWidths[11] = Math.Max(maxColumnWidths[11], data.ElementAt(i).FacultyName.Length);
                maxColumnWidths[12] = Math.Max(maxColumnWidths[12], data.ElementAt(i).MajorName.Length);
                maxColumnWidths[13] = Math.Max(maxColumnWidths[13], data.ElementAt(i).LinkedInProfile.Length);
            }

            for (int col = 1; col < maxColumnWidths.Length; col++)
            {
                worksheet.Cells.SetColumnWidth(col, maxColumnWidths[col] + 5);
            }

            //Master Sheet
            Worksheet masterSheet = workbook.Worksheets.Add("Master");

            //Populate the master sheet with the State and District dropdown values
            for (int i = 0; i < states.Count; i++)
            {
                masterSheet.Cells[i + 1, 0].PutValue(states[i]);
            }

            for (int i = 0; i < districts.Count; i++)
            {
                masterSheet.Cells[i + 1, 1].PutValue(districts[i]);
            }

            masterSheet.VisibilityType = VisibilityType.VeryHidden;

            //Data Validation for State
            int stateColumnIndex = 7;
            CellArea stateCellArea = CellArea.CreateCellArea(1, stateColumnIndex, 1000, stateColumnIndex);
            Validation stateValidation = worksheet.Validations[worksheet.Validations.Add(stateCellArea)];
            stateValidation.Type = ValidationType.List;
            stateValidation.Formula1 = "='Master'!A1:A" + states.Count;

            //Data Validation for District
            int districtColumnIndex = 8;
            CellArea districtCellArea = CellArea.CreateCellArea(1, districtColumnIndex, 1000, districtColumnIndex);
            Validation districtValidation = worksheet.Validations[worksheet.Validations.Add(districtCellArea)];
            districtValidation.Type = ValidationType.List;
            districtValidation.Formula1 = "='Master'!B1:B" + districts.Count;

            //Data Validation for FirstName, MiddleName, Lastname, max length 50
            int firstNameColumnIndex = 1;
            int middlenameColumnIndex = 2;
            int lastNameColumnIndex = 3;
            CellArea nameCellArea = CellArea.CreateCellArea(1, firstNameColumnIndex, 1000, lastNameColumnIndex);
            Validation nameValidation = worksheet.Validations[worksheet.Validations.Add(nameCellArea)];
            nameValidation.Type = ValidationType.TextLength;
            nameValidation.Operator = OperatorType.Between;
            nameValidation.Formula1 = "1";
            nameValidation.Formula2 = "50";
            nameValidation.ShowError = true;
            nameValidation.AlertStyle = ValidationAlertType.Stop;
            nameValidation.ErrorTitle = "Invalid Input";
            nameValidation.ErrorMessage = "Text length must be between 1 and 50 characters";

            return workbook;
        }

        //--------------------------------------------------------------------------------

        public void ImportAlumniFromExcelToDatabase(HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }
    }
}