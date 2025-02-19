using GenericRepository;
using Rentify.Domain.Items;
using Rentify.Infrastructure.Context;

namespace Rentify.Infrastructure.Repositories;
internal sealed class ItemRepository : Repository<Item, ApplicationDbContext>, IItemRepository
{
    public ItemRepository(ApplicationDbContext context) : base(context)
    {
    }
}
