using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AlumniManagement.Models.DTO;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.AlumniImageService;

namespace AlumniManagement.Models.Repositories
{
    public class AlumniImage : IAlumniImage
    {
        private AlumniImageServiceClient _alumniImageServiceClient;

        public AlumniImage()
        {
            _alumniImageServiceClient = new AlumniImageServiceClient();
        }

        IEnumerable<DTO.AlumniImageDTO> IAlumniImage.GetAllImages(int alumniID)
        {
            var data = _alumniImageServiceClient.GetAllImages(alumniID);
            var result = data.Select(a => Mapping.Mapper.Map<DTO.AlumniImageDTO>(a)).ToList();
            return result;
        }

        DTO.AlumniImageDTO IAlumniImage.GetImageByID(int imageID, int alumniID)
        {
            var data = _alumniImageServiceClient.GetImageByID(imageID, alumniID);
            var result = Mapping.Mapper.Map<DTO.AlumniImageDTO>(data);
            return result;
        }

        public async Task AddImage(IEnumerable<DTO.AlumniImageDTO> alumniImages)
        {
            if (alumniImages == null || !alumniImages.Any())
                throw new ArgumentException("No images to add");

            try
            {
                var data = alumniImages.Select(a => Mapping.Mapper.Map<AlumniImageService.AlumniImageDTO>(a)).ToArray();
                _alumniImageServiceClient.AddImage(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding images", ex);
            }
        }

        public async Task DeleteIamgeByIDAsync(int imageID, int alumniID)
        {
            try
            {
                await _alumniImageServiceClient.DeleteImageByIDAsync(imageID, alumniID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting image", ex);
            }
                
        }
    }
}