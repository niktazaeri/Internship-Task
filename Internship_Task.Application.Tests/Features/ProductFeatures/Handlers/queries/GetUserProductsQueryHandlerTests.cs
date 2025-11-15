using AutoMapper;
using FluentAssertions;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features;
using Internship_Task.Application.Features.ProductFeatures.handlers.queries;
using Internship_Task.Application.Features.ProductFeatures.requests.queries;
using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Tests.Features.ProductFeatures.Handlers.queries
{
    public class GetUserProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly MapperConfiguration _mockMapper;
        public GetUserProductsQueryHandlerTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            }
            );
        }
        [Fact]
        public async Task GetUserProducts_IfSuccess()
        {

            var username = "user-1";
            var products = new List<Product>
            {
                new Product {Name = "shampoo" , UserId = "1" , User = new User{ UserName = "user-1" } },
                new Product {Name = "tv" , UserId = "1", User = new User{ UserName = "user-1" }},
                new Product {Name = "test" , UserId = "2", User = new User{ UserName = "user-2" }},
            };

            _mockProductRepo.Setup(x => x.GetUserProductsAsync(username)).ReturnsAsync(
                products.Where(p => p.User.UserName == "user-1").ToList());

            var query = new GetUserProductsQuery { Username = username };
            var handler = new GetUserProductsQueryHandler(_mockProductRepo.Object , _mockMapper.CreateMapper());

            //act
            var result = await handler.Handle(query, CancellationToken.None);

            //assert
            result.Success.Should().BeTrue();
            result.Products.Select(p=>p.UserId).Should().Contain("1");
        }
        [Fact]
        public async Task UserProductsNotFound_ToFetchTheirProducts()
        {
            var username = "user-3";
            var products = new List<Product>
            {
                new Product {Name = "test1" , User = new User {UserName = "user-1" }},
                new Product {Name = "test2" , User = new User {UserName = "user-2" }}
            };
            _mockProductRepo.Setup(x => x.GetUserProductsAsync(username))
                .ReturnsAsync(products.Where(p=>p.User.UserName == username).ToList());

            var query = new GetUserProductsQuery { Username= username };
            var handler = new GetUserProductsQueryHandler(_mockProductRepo.Object , _mockMapper.CreateMapper());

            var result = await handler.Handle(query,CancellationToken.None);
            
            result.Success.Should().BeFalse();
        }
    }
}
