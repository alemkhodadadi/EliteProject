using EliteBlog.Domain.Interfaces;
using EliteBlog.Domain.Models;
using EliteBlog.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Repository.Repositories
{
    class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostID(Guid id)
        {
            return await _context.Comments.Include(x => x.Creator).Where(x => x.PostID == id).ToListAsync();
        }
    }
}
