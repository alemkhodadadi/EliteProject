using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlog.Models.ViewModels
{
    public class BaseViewModel
    {
        public Guid ID { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
        public DateTime? ModifiedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public ApplicationUserViewModel Creator { get; set; }
    }
}
