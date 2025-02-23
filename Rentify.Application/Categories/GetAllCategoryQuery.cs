using MediatR;
using Rentify.Domain.Categories;

namespace Rentify.Application.Categories;
public sealed record GetAllCategoryQuery() : IRequest<List<CategoryDto>>;

public sealed class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}

internal sealed class GetAllCategoryQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoryQuery, List<CategoryDto>>
{
    public Task<List<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = categoryRepository.GetAll().ToList();

        var categoryDtos = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description!
        }).ToList();

        return Task.FromResult(categoryDtos);
    }
}
