using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Models
{
    public class Post : BaseClass
    {
        [Required(ErrorMessage = "Please enter Title")]
        public string Title { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<PostCategory> Categories { get; set; }
    }
}
