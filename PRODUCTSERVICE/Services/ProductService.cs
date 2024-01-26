﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRODUCTSERVICE.Data;
using PRODUCTSERVICE.Models;
using PRODUCTSERVICE.Services.IServices;

namespace PRODUCTSERVICE.Services
{
    public class ProductService : IProducts

    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> AddProduct(Product addedProduct)
        {
           _context.Products.Add(addedProduct);
            await _context.SaveChangesAsync();
            return "Product Added Successfully";
        }

        public async Task<string> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync ();
            return "Product deleted Successfully";
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            return await _context.Products.Where(x => x. Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            var prod = await _context.Products.ToListAsync();
            return prod;
        }

        public async Task<bool> UpdateHighestBid(Guid Id, int newbid)
        {
           var prod = await _context.Products.Where(x => x .Id == Id).FirstOrDefaultAsync();
            if(prod == null || prod.EndTime < DateTime.UtcNow)
            {
                return false;
            }
            if (newbid <= prod.HighestBid)
            {
                return false;
            }
            prod.HighestBid=newbid;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> UpdateProduct(Product product)
        {
            await _context.SaveChangesAsync();
            return "Product Updated";
        }
    }
}
