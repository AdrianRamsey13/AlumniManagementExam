﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlumniManagement.Models.DTO
{
    public class MajorDTO
    {
        [Key]
        public int MajorID { get; set; }

        [Required(ErrorMessage = "Major Name is required")]
        [StringLength(100)]
        [DisplayName("Name")]
        public string MajorName { get; set; }

        [DisplayName("Faculty")]
        public System.Nullable<int> FacultyID { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm:ss tt}")]
        [DisplayName("Last Update")]
        public System.DateTime ModifiedDate { get; set; }

        [DisplayName("Faculty")]
        public string FacultyName { get; set; }

        public List<SelectListItem> DdlFaculty { get; set; }
    }
}