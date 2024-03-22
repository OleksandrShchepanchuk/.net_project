namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class Wish
    {
        public int WishId { get; set; }
        public string WishContent { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
