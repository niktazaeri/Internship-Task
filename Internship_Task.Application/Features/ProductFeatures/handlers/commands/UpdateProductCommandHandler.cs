using AutoMapper;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.ProductFeatures.requests.commands;
using Internship_Task.Application.Interfaces;
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
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductRepository productRepository , IMapper mapper , IProductService productService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productService = productService;
        }
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ProductResponse();
            var existed_product = await _productRepository.GetAsync(request.Id);
            existed_product = _mapper.Map(request.updateProductDTO, existed_product);
            var IsDateEmailUnique = await _productService.IsDateEmailUnique(_mapper.Map<ProductDTO>(existed_product));
            if (IsDateEmailUnique)
            {
                var product = await _productRepository.UpdateAsync(existed_product);

                if (product != null)
                {
                    response.Success = true;
                    response.Message = "Product has been updated successfully.";
                    response.Product = _mapper.Map<ProductDTO>(product);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Updating failed!";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Updating failed! Product date and manufacture email must be unique.";
            }
            return response;
        }
    }
}
