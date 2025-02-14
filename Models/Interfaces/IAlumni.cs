using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AlumniManagement.AlumniService;
using AlumniManagement.Models;

namespace AlumniManagement.Models.Interfaces
{
    public interface IAlumni
    {
        
        IEnumerable<AlumniDTO> GetAlumnis();

        
        AlumniDTO GetAlumniByID(int alumniID);

        
        IEnumerable<AlumniDTO> GetAlumniFullName(string fullName);

        
        void AddAlumni(AlumniDTO alumni);

        
        void UpdateAlumni(AlumniDTO alumni);

        
        void DeleteAlumni(int alumniID);

        
        int GetDistrictIdByName(string districtName);

        
        int GetStateIdByName(string stateName);
    }
}
