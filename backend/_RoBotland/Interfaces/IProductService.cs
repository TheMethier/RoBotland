using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        ProductDto GetProductById(int id);
        int AddNewProduct(ProductDto product);
        void DeleteProduct(int id);
        int UpdateProduct(int id,ProductDto product);
        List<ProductDto> GetFilteredProducts(ProductFilterDto filterParameters);
    }
}
