using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IPostCategoryRepository PostCategories { get; }
        int Complete();
    }
}
