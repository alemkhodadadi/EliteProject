using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Models
{
    public class Comment : BaseClass
    {
        public string Text { get; set; }
        public Guid PostID { get; set; }
    }
}
