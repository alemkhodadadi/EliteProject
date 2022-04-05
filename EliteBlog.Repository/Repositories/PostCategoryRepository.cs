using EliteBlog.Domain.Interfaces;
using EliteBlog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Repository.Repositories
{
    class PostCategoryRepository : GenericRepository<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<PostCategory>> GetPostCategoriesByPostID(Guid id)
        {
            return await _context.PostCategories.Where(c => c.PostID == id).ToListAsync();
        }
    }
}
