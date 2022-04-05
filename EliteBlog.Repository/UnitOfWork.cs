using EliteBlog.Domain.Interfaces;
using EliteBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext AppDbContext;
        public IPostRepository Posts { get; }
        public ICommentRepository Comments { get; }
        public ICategoryRepository Categories { get; }
        public IPostCategoryRepository PostCategories { get; }

        public UnitOfWork(ApplicationDbContext AppDbContext_,
            IPostRepository Posts_,
            ICommentRepository Comments_,
            ICategoryRepository Categories_,
            IPostCategoryRepository PostCategories_)
        {
            this.AppDbContext = AppDbContext_;

            this.Posts = Posts_;
            this.Comments = Comments_;
            this.Categories = Categories_;
            this.PostCategories = PostCategories_;
        }
        public int Complete()
        {
            return AppDbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                AppDbContext.Dispose();
            }
        }
    }
}
