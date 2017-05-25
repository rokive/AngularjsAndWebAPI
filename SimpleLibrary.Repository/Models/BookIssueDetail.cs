using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookIssueDetail:Base
    {
        [Required]
        [Display(Name = "ISBN")]
        
        [MaxLength(13,ErrorMessage="ISBN Maximum lenght 13")]
        [MinLength(10, ErrorMessage = "ISBN Minimum lenght 10")]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Book Title")]
        [MaxLength(100, ErrorMessage = "Book Title Maximum lenght 100")]
        public string BookTitle { get; set; }
        
        [Required]
        [Display(Name = "Author Name")]
        [MaxLength(50, ErrorMessage = "Author Name Maximum lenght 50")]
        public string AuthorName { get; set; }
        
        [Required]
        [Display(Name = "Publisher Name")]
        [MaxLength(50, ErrorMessage = "Publisher Names Maximum lenght 50")]
        public string PublisherName { get; set; }
        
        public bool Return { get; set; }

        public int BookIssueMainId { get; set; }
        public BookIssueMain BookIssueMain { get; set; }
    }
}
