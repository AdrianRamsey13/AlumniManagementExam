﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniManagement.Models.DTO
{
    public class DistrictDTO
    {
        public int DistrictID { get; set; }

        public string DistrictName { get; set; }

        public int StateID { get; set; }

        public string StateName { get; set; }
    }
}