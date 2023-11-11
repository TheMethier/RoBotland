using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        int AddNewProduct(Product product);
        bool DeleteProduct(int id);
        int UpdateProduct(int id,Product product);
    }
}
