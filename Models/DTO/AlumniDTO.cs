using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.Configuration.Annotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AlumniManagement.Models.DTO
{
    public class AlumniDTO
    {
        [Key]
        public int AlumniID { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(15)]
        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [DisplayName("District")]
        public System.Nullable<int> DistrictID { get; set; }

        public string DistrictName { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Date of Birth")]
        public System.DateTime DateOfBirth { get; set; }

        [DisplayName("Graduation Year")]
        public System.Nullable<int> GraduationYear { get; set; }

        [StringLength(100)]
        public string Degree { get; set; }

        [DisplayName("Major")]
        public System.Nullable<int> MajorID { get; set; }

        [DisplayName("LinkedIn Profile")]
        [StringLength(255)]
        public string LinkedInProfile { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm:ss tt}")]
        [DisplayName("Last Update")]
        public System.DateTime ModifiedDate { get; set; }

        // tambahan
        [DisplayName("Address")]
        public string FullAddress { get; set; }

        [DisplayName("Name")]
        public string FullName { get; set; }

        [DisplayName("Faculty")]
        public string FacultyName { get; set; }

        [DisplayName("Major")]
        public string MajorName { get; set; }

        [DisplayName("Faculty")]
        public int FacultyID { get; set; }

        [DisplayName("State")]
        public int StateID { get; set; }

        public IEnumerable<SelectListItem>States { get; set; }

        public IEnumerable<SelectListItem> Districts { get; set; }

        public IEnumerable<SelectListItem> Faculties { get; set;}

        public IEnumerable<SelectListItem> Majors { get; set; }

        public IEnumerable<SelectListItem> Degrees { get; set; }

    }
}