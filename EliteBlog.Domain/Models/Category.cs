using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Models
{
    public class Category : BaseClass
    {
        public string Title { get; set; }
        public IList<PostCategory> Posts { get; set; }
    }
}
