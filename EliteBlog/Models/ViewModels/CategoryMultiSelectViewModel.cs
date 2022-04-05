using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlog.Models.ViewModels
{
    public class CategoryMultiSelectViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public IList<SelectListItem> ItemList { get; set; }
    }
}
