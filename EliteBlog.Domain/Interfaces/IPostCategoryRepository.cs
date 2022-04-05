using EliteBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Interfaces
{
    public interface IPostCategoryRepository : IGenericRepository<PostCategory>
    {
        Task<IEnumerable<PostCategory>> GetPostCategoriesByPostID(Guid id);
    }
}
