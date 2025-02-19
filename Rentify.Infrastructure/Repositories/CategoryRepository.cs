using GenericRepository;
using Rentify.Domain.Categories;
using Rentify.Infrastructure.Context;

namespace Rentify.Infrastructure.Repositories;
internal sealed class CategoryRepository : Repository<Category, ApplicationDbContext>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
