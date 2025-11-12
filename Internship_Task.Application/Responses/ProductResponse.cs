using Internship_Task.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Responses
{
    public class ProductResponse
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public ProductDTO? Product { get; set; }
        public List<ProductDTO>? Products { get; set; }
    }
}
