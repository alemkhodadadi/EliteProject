using EliteBlog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Repository.Seeds
{
    public class SeedCategories
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                    new Category()
                    {
                        ID = Guid.NewGuid(),
                        Title = "Education"
                    },
                    new Category()
                    {
                        ID = Guid.NewGuid(),
                        Title = "Health"
                    },
                    new Category()
                    {
                        ID = Guid.NewGuid(),
                        Title = "Politics"
                    },
                    new Category()
                    {
                        ID = Guid.NewGuid(),
                        Title = "literature"
                    },
                    new Category()
                    {
                        ID = Guid.NewGuid(),
                        Title = "Entertainment"
                    },
                    new Category()
                    {
                        ID = Guid.NewGuid(),
                        Title = "LifeStyle"
                    }
                );
        }
    }
}
