using AutoMapper;
using FluentAssertions;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.ProductFeatures.handlers.commands;
using Internship_Task.Application.Features.ProductFeatures.requests.commands;
using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using Internship_Task.Application.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Internship_Task.Application.Tests.Features.ProductFeature.Handlers.commands
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]
        public async Task UpdateHandler_ShouldReturnSuccess_IfProductUpdated()
        {
            //arrange
            var mockProductRepo = new Mock<IProductRepository>();
            var mockProductService = new Mock<IProductService>();
            var mockMapper = new MapperConfiguration(
                c =>
                {
                    c.CreateMap<UpdateProductDTO, Product>().ReverseMap();
                    c.CreateMap<ProductDTO, Product>().ReverseMap();
                });

            var command = new UpdateProductCommand
            {
                Id = 1,
                updateProductDTO = new UpdateProductDTO
                {
                    Name = "Test",
                    ProductDate = DateTime.Now,
                    ManufactureEmail = "user@example.com",
                    ManufacturePhone = "12345678901",
                    IsAvailable = true,
                }
            };
            var handler = new UpdateProductCommandHandler(mockProductRepo.Object, mockMapper.CreateMapper(), mockProductService.Object);

            mockProductRepo.Setup(x => x.GetAsync(1)).ReturnsAsync(new Product { Name = "Test" });
            mockProductService.Setup(x => x.IsDateEmailUnique(It.IsAny<ProductDTO>())).ReturnsAsync(true);
            mockProductRepo.Setup(x => x.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(new Product { Name = "Test" });

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Success.Should().BeTrue();
            result.Product.Name.Should().Be("Test");
        }
        [Fact]
        public async Task UpdateProductHandler_ShouldReturnUnseccess_IfProductDateEmailNotUnique()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            var mockProductService = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();

            var command = new UpdateProductCommand
            {
                Id = 1,
                updateProductDTO = new UpdateProductDTO
                {
                    Name = "Test",
                    ProductDate = DateTime.Now,
                    ManufactureEmail = "user@example.com",
                    ManufacturePhone = "12345678901",
                    IsAvailable = true,

                }
            };
            var handler = new UpdateProductCommandHandler(mockProductRepo.Object, mockMapper.Object, mockProductService.Object);

            mockProductRepo.Setup(x => x.GetAsync(1)).ReturnsAsync(new Product { Name = "Test" });
            mockProductService.Setup(x => x.IsDateEmailUnique(It.IsAny<ProductDTO>())).ReturnsAsync(false);
            mockProductRepo.Setup(x => x.UpdateAsync(It.IsAny<Product>())).ReturnsAsync((Product)null);

            var result = await handler.Handle(command,CancellationToken.None);

            result.Success.Should().BeFalse();
        }
    }
}
