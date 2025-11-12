using AutoMapper;
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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteAsync(_mapper.Map<Product>(request.productDTO));
            var response = new BaseResponse
            {
                Success = true,
                Message = "Product has been deleted successfully!"
            };
            return response;
        }
    }
}
