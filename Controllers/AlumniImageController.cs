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
        public ActionResult Index(int alumniID)
        {
         
            ViewBag.AlumniID = alumniID;
            var alumniImages = _alumniImageRepository.GetAllImages(alumniID);
            var fullName = _alumniRepository.GetAlumniByID(alumniID).FullName;
            ViewBag.FullName = fullName;
            return View(alumniImages);
        }

        public JsonResult GetAlumniImages(int alumniID)
        {
            ViewBag.AlumniID = alumniID;
            var images = _alumniImageRepository.GetAllImages(alumniID);
            return Json(new { data = images }, JsonRequestBehavior.AllowGet);
        }

        // POST: AlumniImages/Create
        [HttpPost]
        public async Task<ActionResult> UploadImages(int alumniID, HttpPostedFileBase[] files)
        {
            try
            {
                if (files == null || files.Length == 0)
                {
                    TempData["ErrorMessage"] = "Please upload at least one valid file.";
                    return RedirectToAction("Index", new { alumniID = alumniID });
                }

                var alumniImage = new List<AlumniImageDTO>();

                foreach (var file in files)
                {
                    if (file.ContentLength > fileSizeLimit) // 10 MB
                    {
                        TempData["ErrorMessage"] = "File size must be less than 10 MB.";
                        return RedirectToAction("Index", new { alumniID = alumniID });
                    }

                    var extension = Path.GetExtension(file.FileName).ToLower();
                    if (extension != ".jpeg" && extension != ".png" && extension != ".jpg")
                    {
                        TempData["ErrorMessage"] = "Only .jpeg, jpg and .png files are allowed.";
                        return RedirectToAction("Index", new { alumniID = alumniID });
                    }

                    var fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(Server.MapPath(alumniImagesPath), fileName);

                    file.SaveAs(filePath);  // Using SaveAs instead of CopyToAsync

                    var image = new AlumniImageDTO
                    {
                        AlumniID = alumniID,
                        ImagePath =alumniImagesPath,
                        FileName = fileName,
                        UploadDate = DateTime.Now
                    };
                    alumniImage.Add(image);
                }

                await _alumniImageRepository.AddImage(alumniImage);
                TempData["SuccessMessage"] = "Images uploaded successfully.";
                return RedirectToAction("Index", new { alumniID = alumniID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while uploading the images: " + ex.Message;
                return RedirectToAction("Index", new { alumniID = alumniID });
            }
        }

        // POST: AlumniImages/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteImage(int id, int alumniID)
        {
            try
            {
                var image = _alumniImageRepository.GetImageByID(id, alumniID);
                if (image == null)
                {
                    ModelState.AddModelError("", "Image not found.");
                    return RedirectToAction("Index", new { alumniID = alumniID });
                }
                else
                {
                    var filePath = Path.Combine(Server.MapPath(alumniImagesPath), image.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    await _alumniImageRepository.DeleteIamgeByIDAsync(id, alumniID);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting image due to " + ex.Message);
            }
            return RedirectToAction("Index", new { alumniID = alumniID });
        }

        public async Task<ActionResult> DeleteSelectedImages(int alumniId, List<int> selectedImages)
        {
            try
            {
                if (selectedImages == null || selectedImages.Count == 0)
                {
                    ModelState.AddModelError("", "Please select at least one image to delete.");
                    return RedirectToAction("Index", new { alumniID = alumniId });
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
            return RedirectToAction("Index", new { alumniID = alumniId });
        }
    }
}