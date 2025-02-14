using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniManagement.FacultyService;

namespace AlumniManagement.Models.Interfaces
{
    public interface IFaculty
    {
        IEnumerable<FacultyDTO> GetFaculties();
        FacultyDTO GetFacultyByID(int facultyID);
        FacultyDTO GetFacultyIdByName(string facultyName);
        void AddFaculty(DTO.FacultyDTO facultyDTO);
        void UpdateFaculty(DTO.FacultyDTO facultyDTO);
        void DeleteFaculty(int id);
    }
}
