using PRODUCTSERVICE.Models.Dtos;

namespace PRODUCTSERVICE.Services.IServices
{
    public interface ICategory
    {
        Task<CategotyDto> GetCategoryById(Guid CategoryId,string token);
    }
}
