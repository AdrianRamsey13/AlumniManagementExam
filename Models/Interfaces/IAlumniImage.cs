using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AlumniManagement.Models.DTO;

namespace AlumniManagement.Models.Interfaces
{
    public interface IAlumniImage
    {
        IEnumerable<AlumniImageDTO> GetAllImages(int alumniID);

        AlumniImageDTO GetImageByID(int imageID, int alumniID);

        
        Task AddImage(IEnumerable<AlumniImageDTO> alumniImages);

        
        Task DeleteIamgeByIDAsync(int imageID, int alumniID);
    }
}
