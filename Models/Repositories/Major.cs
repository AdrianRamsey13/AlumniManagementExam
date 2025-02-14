using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniManagement.MajorService;
using AlumniManagement.Models.Interfaces;

namespace AlumniManagement.Models.Repositories
{
    public class Major : IMajor
    {
        private MajorServicesClient _majorClient;

        public Major()
        {
            _majorClient = new MajorServicesClient();
        }

        public IEnumerable<MajorService.MajorDTO> GetMajors()
        {
            var data = _majorClient.GetMajors();
            var result = data.Select(m => Mapping.Mapper.Map<MajorDTO>(m));
            return result;
        }
        public MajorService.MajorDTO GetMajorByID(int majorID)
        {
            var major = _majorClient.GetMajorByID(majorID);
            return Mapping.Mapper.Map<MajorDTO>(major);
        }

        public MajorService.MajorDTO GetMajorIdByName(string majorName)
        {
            var major = _majorClient.GetMajorIdByName(majorName);
            return Mapping.Mapper.Map<MajorDTO>(major);
        }
        public void AddMajor(DTO.MajorDTO majorDTO)
        {
            var result = Mapping.Mapper.Map<MajorService.MajorDTO>(majorDTO);
            _majorClient.AddMajor(result);
        }

        public void UpdateMajor(DTO.MajorDTO majorDTO)
        {
            var result = Mapping.Mapper.Map<MajorService.MajorDTO>(majorDTO);
            _majorClient.UpdateMajor(result);
        }
        public void DeleteMajor(int majorID)
        {
            _majorClient.DeleteMajor(majorID);
        }
    }
}