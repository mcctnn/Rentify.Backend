using GenericRepository;
using MediatR;
using Rentify.Domain.Categories;
using TS.Result;

namespace Rentify.Application.Categories;

public sealed record DeleteCategoryCommand(
    Guid Id):IRequest<Result<string>>;

internal sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    public async Task<Result<String>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (category is null)
            return Result<string>.Failure("Category not found");

        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Category has been deleted");
    }
}
