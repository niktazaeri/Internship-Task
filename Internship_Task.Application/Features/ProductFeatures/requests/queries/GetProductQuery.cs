using Internship_Task.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Features.ProductFeatures.requests.queries
{
    public class GetProductQuery:IRequest<ProductResponse>
    {
        public int Id { get; set; }
    }
}
