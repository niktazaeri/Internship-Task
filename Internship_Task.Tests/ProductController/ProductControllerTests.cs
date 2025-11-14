using FluentAssertions;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.ProductFeatures.requests.commands;
using Internship_Task.Application.Features.ProductFeatures.requests.queries;
using Internship_Task.Application.Responses;
using Internship_Task.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System.Security.Claims;

namespace Internship_Task.Tests;

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly ProductController _productController;

    public ProductControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _productController = new ProductController(_mockMediator.Object);

        _productController.ControllerContext = new ControllerContext();
        _productController.ControllerContext.HttpContext = new DefaultHttpContext();

        var user = new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier , "user-id")
                }
                )
        );

        _productController.HttpContext.User = user;

    }
    //Create
    [Fact]
    public async Task NotCreateProduct_IfDateEmailNotUnique()
    {
        var product = new CreateProductDTO
        {
            IsAvailable = true,
            Name = "book",
            ManufactureEmail = "user@yahoo.com",
            ProductDate = DateTime.Now
        };
        var command = new CreateProductCommand
        {
            createProductDTO = product,
            UserId = "user-id"
        };
        _mockMediator.Setup(x=>x.Send(It.IsAny<CreateProductCommand>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductResponse { Success = false });

        var result = await _productController.CreateProduct(product);

        //assert
        result.Should().BeOfType<BadRequestObjectResult>();

    }
    //Read
    [Fact]
    public async Task ProductNotFound()
    {
        var query = new GetProductQuery { Id = 1 };
        _mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductResponse { Success = false});
        var result1 = await _productController.GetProduct(1);
        var result2 = await _productController.GetProduct(0);

        result1.Should().BeOfType<NotFoundObjectResult>();
        result2.Should().BeOfType<BadRequestObjectResult>();
    }
    //Update
    [Fact]
    public async Task ForbiddenUser_ToUpdateProduct()
    {
        var update_product = new UpdateProductDTO
        {
            Name = "updated"
        };
        _mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new ProductResponse { Success = true ,Product = new ProductDTO { Id = 1, UserId = "unknown" } });

        var result = await _productController.EditProduct(update_product, 1);

        result.Should().BeOfType<ForbidResult>();
    }

    //Delete
    [Fact]
    public async Task DeleteProduct_IfItsForUser()
    {

        var query = new GetProductQuery { Id = 2 };

        _mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new ProductResponse { Product = new ProductDTO { Id = 2 , UserId = "user-id"}
        , Success = true});


        var command = new DeleteProductCommand { productDTO = new ProductDTO { Id = 2}  };
        _mockMediator.Setup(x=>x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BaseResponse { Success = true });
        //act
        var result = await _productController.DeleteProduct(2);
        //assert
        result.Should().BeOfType<NoContentResult>();
    }
}
