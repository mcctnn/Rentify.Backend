﻿using GenericRepository;
using Mapster;
using MediatR;
using Rentify.Domain.Categories;
using Rentify.Domain.Items;
using TS.Result;

namespace Rentify.Application.Items;
public sealed record CreateItemCommand(
    Guid UserId,
    string Name,
    string ItemImageUrl,
    decimal PricePerDay,
    bool IsAvailable,
    string Description,
    Guid CategoryId,
    string Location) :IRequest<Result<string>>;


internal sealed class CreateItemCommandHandler(
    IItemRepository itemRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateItemCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByExpressionAsync(p => p.Id == request.CategoryId,cancellationToken);
        bool isCategoryNameExists = await categoryRepository.AnyAsync(p => p.Id == request.CategoryId, cancellationToken);

        if (!isCategoryNameExists)
            return Result<string>.Failure("Böyle bir kategori bulunamadı");


        Item item=request.Adapt<Item>();
        item.CategoryId = request.CategoryId;
            
        itemRepository.Add(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Eşya başarıyla kaydedildi");
    }
}

