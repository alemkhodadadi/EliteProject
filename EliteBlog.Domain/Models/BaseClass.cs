using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Models
{
    public class BaseClass
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser Creator { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
