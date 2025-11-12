using AutoMapper;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.ProductFeatures.requests.commands;
using Internship_Task.Application.Responses;
using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Features.ProductFeatures.handlers.commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository , IUserRepository userRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.createProductDTO);
            product.UserId = request.UserId;
            var new_product = await _productRepository.CreateAsync(product);
            var response = new ProductResponse();
            if(new_product != null)
            {
                response.Success = true;
                response.Message = "Product has been created successfully.";
                response.Product = _mapper.Map<ProductDTO>(new_product);
            }
            else
            {
                response.Success = false;
                response.Message = "Creating product failed! Product date and manufactur email must be unique. ";
            }
            return response;
        }
    }
}
