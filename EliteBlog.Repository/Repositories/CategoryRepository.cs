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
    class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Category>> GetCategoriesByPostID (Guid id)
        {
            var postCategories = await _context.PostCategories.Where(x => x.PostID == id).ToListAsync();
            var categoriesByPostID = new List<Category>();
            foreach(var postCategory in postCategories)
            {
                var category = await _context.Categories.FindAsync(postCategory.CategoryID);
                categoriesByPostID.Add(category);
            }
            return categoriesByPostID;
        }
    }
}
