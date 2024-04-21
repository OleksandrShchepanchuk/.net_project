namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class DepartmentDTO
    {

        //public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Speciality { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
