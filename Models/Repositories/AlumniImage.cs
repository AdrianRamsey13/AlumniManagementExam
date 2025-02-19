using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AlumniManagement.Models.DTO;
using AlumniManagement.Models.Interfaces;

namespace AlumniManagement.Models.Repositories
{
    public class AlumniImage : IAlumniImage
    {
        public Task AddImage(IEnumerable<AlumniImageDTO> alumniImages)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIamgeByIDAsync(int imageID, int alumniID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlumniImageDTO> GetAllImages(int alumniID)
        {
            throw new NotImplementedException();
        }

        public AlumniImageDTO GetImageByID(int imageID, int alumniID)
        {
            throw new NotImplementedException();
        }
    }
}