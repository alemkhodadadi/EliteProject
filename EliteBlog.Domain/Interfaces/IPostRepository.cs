using EliteBlog.Domain.Models;
using EliteBlog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByPostName(string orderName);
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostWithComments(Guid id);
    }
}
