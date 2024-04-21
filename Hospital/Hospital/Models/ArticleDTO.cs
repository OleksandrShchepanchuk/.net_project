namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class ArticleDTO
    {
        //public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        //public string Image { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
