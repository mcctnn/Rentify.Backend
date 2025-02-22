using MediatR;
using Rentify.Domain.Categories;
using TS.Result;

namespace Rentify.Application.Categories;
public sealed record GetCategoryQuery(
    Guid Id):IRequest<Result<Category>>;

internal sealed class GetCategoryQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, Result<Category>>
{
    public async Task<Result<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (category is null)
            return Result<Category>.Failure("Kategori bulunamadı");

        return category;
    }
}
