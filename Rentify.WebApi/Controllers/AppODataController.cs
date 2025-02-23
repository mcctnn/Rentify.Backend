using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Rentify.Application.Categories;
using Rentify.Application.Items;
using Rentify.Application.Users;
using Rentify.Domain.Items;
using Rentify.Domain.Users;
using TS.Result;

namespace Rentify.WebApi.Controllers;

[Route("odata")]
[ApiController]
[EnableQuery]
public class AppODataController(ISender sender) : ODataController
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();
        builder.EnableLowerCamelCase();
        builder.EntitySet<CategoryDto>("categories");
        builder.EntitySet<Item>("items");
        builder.EntitySet<User>("users");
        return builder.GetEdmModel();
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public async Task<List<CategoryDto>> GetAllCategories(CancellationToken token)
    {
        var response = await sender.Send(new GetAllCategoryQuery(), token);
        return response;
    }

    [HttpGet("items")]
    public async Task<Result<List<Item>>> GetAllItems(CancellationToken token)
    {
        var response = await sender.Send(new GetAllItemQuery(), token);
        return response;
    }

    [HttpGet("users")]
    public async Task<Result<List<User>>> GetAllUsers(CancellationToken token)
    {
        var response = await sender.Send(new GetAllUserQuery(), token);
        return response;
    }
}
