namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
        //public int DoctorId { get; set; }
        //public Doctor Doctor { get; set; }
    }
}
