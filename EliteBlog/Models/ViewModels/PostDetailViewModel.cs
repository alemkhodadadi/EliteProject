using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlog.Models.ViewModels
{
    public class PostDetailViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
        public IList<CategoryViewModel> Categories { get; set; }
    }
}
