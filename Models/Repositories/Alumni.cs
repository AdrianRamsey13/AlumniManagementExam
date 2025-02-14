using AlumniManagement.AlumniService;
using AlumniManagement.FacultyService;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniManagement.Models.Repositories
{
    public class Alumni : IAlumni
    {
        private AlumniServicesClient _alumniServiceClient;

        public Alumni()
        {
            _alumniServiceClient = new AlumniServicesClient();
        }

        public IEnumerable<AlumniDTO> GetAlumnis()
        {
            var data = _alumniServiceClient.GetAlumnis();
            var result = data.Select(a => Mapping.Mapper.Map<AlumniDTO>(a)).ToList();
            return result;
        }
        public AlumniDTO GetAlumniByID(int alumniID)
        {
            var data = _alumniServiceClient.GetAlumniByID(alumniID);
            var result = Mapping.Mapper.Map<AlumniDTO>(data);
            return result;
        }
        public int GetDistrictIdByName(string districtName)
        {
            var data = _alumniServiceClient.GetDistrictIdByName(districtName);
            return data;
        }

        public int GetStateIdByName(string stateName)
        {
            var data = _alumniServiceClient.GetStateIdByName(stateName);
            return data;
        }
        public IEnumerable<AlumniDTO> GetAlumniFullName(string fullName)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------

        public void AddAlumni(DTO.AlumniDTO alumni)
        {
            var result = Mapping.Mapper.Map<AlumniService.AlumniDTO>(alumni);
            _alumniServiceClient.AddAlumni(result);
        }

        public void DeleteAlumni(int alumniID)
        {
            _alumniServiceClient.DeleteAlumni(alumniID);
        }        
               
        public void UpdateAlumni(DTO.AlumniDTO alumni)
        {
            var result = Mapping.Mapper.Map<AlumniService.AlumniDTO>(alumni);
            _alumniServiceClient.UpdateAlumni(result);
        }
    }
}