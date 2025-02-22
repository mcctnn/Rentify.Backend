using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using Rentify.Domain.Categories;
using TS.Result;

namespace Rentify.Application.Categories;
public sealed record CreateCategoryCommand(
    string Name,
    string Description):IRequest<Result<string>>;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        bool isCategoryNameExists = await categoryRepository.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower(), cancellationToken);

        if (isCategoryNameExists)
            return Result<string>.Failure("Aynı isimle kayıt yapılamaz");

        Category category =request.Adapt<Category>();

        categoryRepository.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Kategori başarıyla oluşturuldu");
    }
}

//public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
//{
//    public CreateCategoryCommandValidator()
//    {
//        throw new NotImplementedException();
//    }
//}
