using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniManagement.DistrictService;

namespace AlumniManagement.Models.Interfaces
{
    public interface IDistrict
    {
        IEnumerable<DistrictDTO> GetDistrict(int stateID);
    }
}
