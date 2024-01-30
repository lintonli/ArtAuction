using PRODUCTSERVICE.Models;
using PRODUCTSERVICE.Models.Dtos;

namespace PRODUCTSERVICE.Services.IServices
{
    public interface IProducts
    {
        Task<List<Product>> GetProducts();
        Task<string> AddProduct(Product addedProduct);
        Task<Product> GetProductById(Guid Id);
        Task<string> DeleteProduct(Product product);
        Task<string> UpdateProduct(Product product);
        Task<bool> UpdateHighestBid(Guid Id, int newbid);
 
    }
}
