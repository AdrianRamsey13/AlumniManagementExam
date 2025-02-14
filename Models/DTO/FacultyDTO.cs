using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniManagement.FacultyService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace AlumniManagement.Models.DTO
{
    public class FacultyDTO
    {
        [Key]
        public int FacultyID { get; set; }

        [DisplayName("Faculty Name")]
        [Required(ErrorMessage = "Faculty Name is required")]
        [StringLength(50, ErrorMessage = "Faculty Name cannot be longer than 50 characters")]
        public string FacultyName { get; set; }

        [DisplayName("Faculty Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd-MMM-yyyy hh:mm:ss tt")]
        [DisplayName("Last Update")]
        public System.DateTime ModifiedDate { get; set; }
    }
}