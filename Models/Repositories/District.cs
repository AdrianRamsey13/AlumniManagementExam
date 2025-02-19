using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.DistrictService;

namespace AlumniManagement.Models.Repositories
{
    public class District : IDistrict
    {
        private DistrictServiceClient _districtServiceClient;
        private AlumniDataContext _context;

        public District()
        {
            _districtServiceClient = new DistrictServiceClient();
        }

        public IEnumerable<DistrictDTO> GetDistrict(int stateID)
        {
            var district = _districtServiceClient.GetDistrictByStateID(stateID);
            return district;
        }
    }
}