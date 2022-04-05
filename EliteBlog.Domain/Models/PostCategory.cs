using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Models
{
    public class PostCategory : BaseClass
    {
        public Guid CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        public Guid PostID { get; set; }
        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }
    }
}
