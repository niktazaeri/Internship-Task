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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository , IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existed_product = await _productRepository.GetAsync(request.Id);
            existed_product.Name = request.updateProductDTO.Name;
            existed_product.ManufacturePhone = request.updateProductDTO.ManufacturePhone;
            existed_product.ManufactureEmail = request.updateProductDTO.ManufactureEmail;
            existed_product.ProductDate = request.updateProductDTO.ProductDate;
            existed_product.IsAvailable = request.updateProductDTO.IsAvailable;
            var product = await _productRepository.UpdateAsync(existed_product);
            
            var response = new ProductResponse();
            if (product != null)
            {
                response.Success = true;
                response.Message = "Product has been updated successfully.";
                response.Product = _mapper.Map<ProductDTO>(product);
            }
            else
            {
                response.Success = true;
                response.Message = "Updating failed! Product date and manufacture email must be unique.";
            }
            return response;
        }
    }
}
