using GenericRepository;
using Mapster;
using MediatR;
using Rentify.Domain.Categories;
using TS.Result;

namespace Rentify.Application.Categories;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string Description):IRequest<Result<string>>;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category=await categoryRepository.FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

        if (category is null)
        {
            return Result<string>.Failure("Category not found");
        }

        category.Description=request.Description;
        category.Name = request.Name;

        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Category updated successful";
    }
}
