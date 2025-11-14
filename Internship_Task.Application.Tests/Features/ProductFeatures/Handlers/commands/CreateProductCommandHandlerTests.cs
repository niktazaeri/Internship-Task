using AutoMapper;
using FluentAssertions;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.ProductFeatures.handlers.commands;
using Internship_Task.Application.Features.ProductFeatures.requests.commands;
using Internship_Task.Application.Interfaces;
using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Tests.Features.ProductFeatures.Handlers.commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<IProductService> _mockProductService;
        private readonly MapperConfiguration _mapper;
        public CreateProductCommandHandlerTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockProductService = new Mock<IProductService>();
            _mapper = new MapperConfiguration(
                c =>
                {
                    c.CreateMap<Product,CreateProductDTO>().ReverseMap();
                    c.CreateMap<Product, ProductDTO>().ReverseMap();
                });
        }
        [Fact]
        public async Task NotCreatedProduct_IfDateEmailNotUnique()
        {

            _mockProductService.Setup(x=>x.IsDateEmailUnique(It.IsAny<ProductDTO>()))
                .ReturnsAsync(false);
            var command = new CreateProductCommand
            {
                createProductDTO = new CreateProductDTO
                {
                    Name = "Test"
                },
                UserId = "user-test"
            };

            var handler = new CreateProductCommandHandler(_mapper.CreateMapper() , _mockProductRepo.Object
                , _mockProductService.Object);

            //act
            var result = await handler.Handle(command , CancellationToken.None);
            //assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("must be unique");
        }
    }
}
