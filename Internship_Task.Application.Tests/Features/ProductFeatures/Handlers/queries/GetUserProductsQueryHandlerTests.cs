using AutoMapper;
using FluentAssertions;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features;
using Internship_Task.Application.Features.ProductFeatures.handlers.queries;
using Internship_Task.Application.Features.ProductFeatures.requests.queries;
using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
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
        [Fact]
        public async Task GetUserProducts_IfSuccess()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            var mockMapper = new MapperConfiguration(
                c =>
                {
                    c.CreateMap<Product, ProductDTO>();
                });

            var userId = Guid.NewGuid().ToString();
            var products = new List<Product>
            {
                new Product {Name = "tv" , IsAvailable = true , ManufactureEmail = "user1@example.com" ,
                    ManufacturePhone = "09999999999" , ProductDate = DateTime.Now , UserId = userId},
                new Product {Name = "phone" , IsAvailable = false , ManufactureEmail = "user1@example.com" ,
                    ManufacturePhone = "09999999999" , ProductDate = DateTime.Now , UserId = userId},
                new Product {Name = "phone2" , IsAvailable = true , ManufactureEmail = "user2@example.com" ,
                    ManufacturePhone = "09999999999" , ProductDate = DateTime.Now , UserId = "user2-id"},
            };

            mockProductRepo.Setup(x => x.GetUserProductsAsync(userId)).ReturnsAsync(
                products.Where(p=>p.UserId == userId).ToList());

            var query = new GetUserProductsQuery
            {
                UserId = userId,
            };

            var handler = new GetUserProductsQueryHandler(mockProductRepo.Object , mockMapper.CreateMapper());

            var result = await handler.Handle(query,CancellationToken.None);

            result.Products.Should().OnlyContain(p => p.UserId == userId);
            result.Success.Should().BeTrue();
        }
    }
}
