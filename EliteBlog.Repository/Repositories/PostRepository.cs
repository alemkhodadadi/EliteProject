using EliteBlog.Domain.Interfaces;
using EliteBlog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using EliteBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Repository.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Post>> GetPostsByPostName(string text)
        {
            return await _context.Posts.Where(c => c.Text.Contains(text)).ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _context.Posts.Include(x=>x.Categories).Include(x=>x.Creator).ToListAsync();
        }

        public async Task<Post> GetPostWithComments(Guid id)
        {
            return await _context.Posts.Include(x => x.Comments).FirstAsync(x=>x.ID == id);
        }
    }
}
