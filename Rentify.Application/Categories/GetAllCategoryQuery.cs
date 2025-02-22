using MediatR;
using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Categories;
using Rentify.Domain.Users;
using TS.Result;

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
        var categories = categoryRepository.GetAll();

        var categoryDtos = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToList();

        return Task.FromResult(categoryDtos);
    }
}
