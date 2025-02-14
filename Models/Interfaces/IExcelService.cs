using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Aspose.Cells;

namespace AlumniManagement.Models.Interfaces
{
    public  interface IExcelService
    {
        Workbook AlumniExportToExcel();

        void ImportAlumniFromExcelToDatabase(HttpPostedFileBase file);
    }
}
