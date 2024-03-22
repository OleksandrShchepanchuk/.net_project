namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
