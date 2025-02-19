using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.FacultyService;

namespace AlumniManagement.Models.Repositories
{
    public class Faculty : IFaculty
    {
        private FacultyServicesClient _facultyClient;

        public Faculty()
        {
            _facultyClient = new FacultyServicesClient();
        }

        public IEnumerable<FacultyService.FacultyDTO> GetFaculties()
        {
            var data = _facultyClient.GetFaculties();
            var result = data.Select(f => Mapping.Mapper.Map<FacultyDTO>(f));
            return result;
        }
        public FacultyService.FacultyDTO GetFacultyByID(int facultyID)
        {
            var data = _facultyClient.GetFacultyByID(facultyID);
            return Mapping.Mapper.Map<FacultyDTO>(data);
        }
        public FacultyDTO GetFacultyIdByName(string facultyName)
        {
            var data = _facultyClient.GetFacultyIdByName(facultyName);
            var result = Mapping.Mapper.Map<FacultyDTO>(data);
            return result;
        }

        public void AddFaculty(DTO.FacultyDTO facultyDTO)
        {
            var faculty = new FacultyService.FacultyDTO
            {
                FacultyID = facultyDTO.FacultyID,
                FacultyName = facultyDTO.FacultyName,
                Description = facultyDTO.Description,
                ModifiedDate = DateTime.Now
            };
            _facultyClient.AddFaculty(faculty);
        }

        public void UpdateFaculty(DTO.FacultyDTO facultyDTO)
        {
            //var faculty = new FacultyService.FacultyDTO
            //{
            //    FacultyID = facultyDTO.FacultyID,
            //    FacultyName = facultyDTO.FacultyName,
            //    Description = facultyDTO.Description,
            //    ModifiedDate = DateTime.Now
            //};

            //_facultyClient.UpdateFaculty(faculty);

            var updateData = Mapping.Mapper.Map<DTO.FacultyDTO, FacultyService.FacultyDTO>(facultyDTO);
            _facultyClient.UpdateFaculty(updateData);
        }
        public void DeleteFaculty(int id)
        {
            _facultyClient.DeleteFaculty(id);
        }

    }
}