using AlumniManagement.Models.DTO;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.Models.Repositories;
//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AlumniManagement.Web.Controllers
{
    public class AlumniImageController : Controller
    {
        public IAlumniImage _alumniImageRepository;
        public IAlumni _alumniRepository;
        public int fileSizeLimit = Convert.ToInt32(ConfigurationManager.AppSettings["FileSizeLimit"]); //10 * 1024 * 1024; // 10 MB
        public string fileTypes = ConfigurationManager.AppSettings["FileTypes"]; //".jpeg,.png";
        public string alumniImagesPath = ConfigurationManager.AppSettings["AlumniImagesPath"]; //"~/images/alumni/";

        public AlumniImageController() : this(new Models.Repositories.AlumniImage(), new Models.Repositories.Alumni())
        {
        }

        public AlumniImageController(AlumniImage alumniImageRepository, Alumni alumniRepository)
        {
            _alumniImageRepository = alumniImageRepository;
            _alumniRepository = alumniRepository;
        }

        // GET: AlumniImages
        public ActionResult Index(int id)
        {
            var alumniImages = _alumniRepository.GetAlumniByID(id); //.AlumniImages;
            return View(alumniImages);
        }

        // POST: AlumniImages/Create
        [HttpPost]
        public async Task<ActionResult> UploadImages(int alumniId, HttpPostedFileBase[] files)
        {
            try
            {
                if (files == null || files.Length == 0)
                {
                    TempData["ErrorMessage"] = "Please upload at least one valid file.";
                    return RedirectToAction("Index", new { id = alumniId });
                }

                var alumniImage = new List<AlumniImageDTO>();

                foreach (var file in files)
                {
                    if (file.ContentLength > fileSizeLimit) // 10 MB
                    {
                        TempData["ErrorMessage"] = "File size must be less than 10 MB.";
                        return RedirectToAction("Index", new { id = alumniId });
                    }

                    var extension = Path.GetExtension(file.FileName).ToLower();
                    if (extension != ".jpeg" && extension != ".png")
                    {
                        TempData["ErrorMessage"] = "Only .jpeg and .png files are allowed.";
                        return RedirectToAction("Index", new { id = alumniId });
                    }

                    var fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(Server.MapPath(alumniImagesPath), fileName);

                    file.SaveAs(filePath);  // Using SaveAs instead of CopyToAsync

                    var image = new AlumniImageDTO
                    {
                        AlumniID = alumniId,
                        ImagePath = Server.MapPath(alumniImagesPath),
                        FileName = fileName,
                        UploadDate = DateTime.Now
                    };
                    alumniImage.Add(image);
                }

                await _alumniImageRepository.AddImage(alumniImage);
                TempData["SuccessMessage"] = "Images uploaded successfully.";
                return RedirectToAction("Index", new { id = alumniId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while uploading the images: " + ex.Message;
                return RedirectToAction("Index", new { id = alumniId });
            }
        }

        // POST: AlumniImages/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteImage(int id, int alumniId)
        {
            try
            {
                var image = _alumniImageRepository.GetImageByID(id, alumniId);
                if (image == null)
                {
                    ModelState.AddModelError("", "Image not found.");
                    return RedirectToAction("Index", new { id = alumniId });
                }
                else
                {
                    var filePath = Path.Combine(Server.MapPath(alumniImagesPath), image.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    await _alumniImageRepository.DeleteIamgeByIDAsync(id, alumniId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting image due to " + ex.Message);
            }
            return RedirectToAction("Index", new { id = alumniId });
        }

        public async Task<ActionResult> DeleteSelectedImages(int alumniId, List<int> selectedImages)
        {
            try
            {
                if (selectedImages == null || selectedImages.Count == 0)
                {
                    ModelState.AddModelError("", "Please select at least one image to delete.");
                    return RedirectToAction("Index", new { id = alumniId });
                }
                foreach (var imageId in selectedImages)
                {
                    await _alumniImageRepository.DeleteIamgeByIDAsync(imageId, alumniId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting image due to " + ex.Message);
            }
            return RedirectToAction("Index", new { id = alumniId });
        }
    }
}