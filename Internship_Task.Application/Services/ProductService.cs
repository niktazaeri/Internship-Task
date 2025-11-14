using Internship_Task.Application.DTOs;
using Internship_Task.Application.Interfaces;
using Internship_Task.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> IsDateEmailUnique(ProductDTO productDTO)
        {
            var products = await _productRepository.GetAllAsync();
            for(int i=0; i<products.Count; i++)
            {
                if (products[i].Id == productDTO.Id)
                    continue;
                else
                {
                    if (products[i].ProductDate == productDTO.ProductDate && products[i].ManufactureEmail == productDTO.ManufactureEmail)
                        return false;
                }
            }
            return true;
        }
    }
}
