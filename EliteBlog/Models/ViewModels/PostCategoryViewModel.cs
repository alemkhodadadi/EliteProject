using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlog.Models.ViewModels
{
    public class PostCategoryViewModel : BaseViewModel
    {
        public Guid CategoryID { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid PostID { get; set; }
        public PostViewModel Post { get; set; }
    }
}
