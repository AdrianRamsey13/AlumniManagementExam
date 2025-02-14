using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniManagement.MajorService;

namespace AlumniManagement.Models.Interfaces
{
    public interface IMajor
    {
        IEnumerable<MajorDTO> GetMajors();

        MajorDTO GetMajorByID(int majorID);

        MajorDTO GetMajorIdByName(string majorName);

        void AddMajor(DTO.MajorDTO majorDTO);

        void UpdateMajor(DTO.MajorDTO majorDTO);

        void DeleteMajor(int majorID);
    }
}
