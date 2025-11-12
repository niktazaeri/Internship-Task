using AutoMapper;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.ProductFeatures.requests.queries;
using Internship_Task.Application.Responses;
using Internship_Task.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Features.ProductFeatures.handlers.queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.Id);
            var response = new ProductResponse();
            if (product == null)
            {
                response.Success = false;
                response.Message = $"product with id = {request.Id} not found.";
            }
            else
            {
                response.Success = true;
                response.Product = _mapper.Map<ProductDTO>(product);
            }
            return response;
        }
    }
}
