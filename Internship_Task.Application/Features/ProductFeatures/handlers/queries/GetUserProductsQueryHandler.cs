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
    public class GetUserProductsQueryHandler : IRequestHandler<GetUserProductsQuery , ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetUserProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(GetUserProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetUserProductsAsync(request.Username);
            var response = new ProductResponse();
            if (products.Count > 0)
            {
                response.Success = true;
                response.Products = _mapper.Map<List<ProductDTO>>(products);
            }
            else
            {
                response.Success = false;
                response.Message = "This user product list is empty";
            }
            return response;
        }
    }
}
