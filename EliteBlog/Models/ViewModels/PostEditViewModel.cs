using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlog.Models.ViewModels
{
    public class PostEditViewModel
    {
        public PostViewModel Post { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
