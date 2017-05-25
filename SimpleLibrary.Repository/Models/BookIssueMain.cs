using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookIssueMain:Base
    {
        
        public int StudentId { get; set; }
        public DateTime IssueDate { get; set; }
        public virtual ICollection<BookIssueDetail> BookIssueDetails { get; set; }
    }
}
