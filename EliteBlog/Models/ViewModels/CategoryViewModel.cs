using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlog.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public IList<PostCategoryViewModel> Posts { get; set; }
    }
}
