using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using Internship_Task.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            var IsExist = await _db.Products.FirstOrDefaultAsync(p => p.ManufactureEmail == product.ManufactureEmail 
            && p.ProductDate == product.ProductDate);
            if (IsExist == null)
            {
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
                return product;
            }
            else
                return null;
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _db.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
            
        }
    }
}
