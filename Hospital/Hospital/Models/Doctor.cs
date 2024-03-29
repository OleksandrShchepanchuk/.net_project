﻿namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Experience { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public int DepartmentId { get; set; } 

        public Department Department { get; set; } 
    }
}