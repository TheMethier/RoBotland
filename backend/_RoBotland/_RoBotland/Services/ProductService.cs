using _RoBotland.Interfaces;
using _RoBotland.Models;

namespace _RoBotland.Services;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;

    public ProductService(DataContext dataContext)
    {
        this._dataContext=dataContext;
    }
    
    public int AddNewProduct(Product product)
    {
        _dataContext.Products.Add(product);
        _dataContext.SaveChanges();
        return product.Id;
    }

    public bool DeleteProduct(int id)
    {
        Product product = _dataContext.Products.Find(id) ?? new Product();
        if (product.Id!=-1)
        {
            _dataContext.Remove(product);
            _dataContext.SaveChanges();
            return true;
        }
        return false;
    }

    public int UpdateProduct(int id, Product product)
    {
        Product oldProduct = _dataContext.Products.Find(id) ?? new Product();
        if (oldProduct.Id != -1)
        {
            _dataContext.Products.Update(product);
        }

        return oldProduct.Id;
    }

    public Product GetProductById(int id)
    {
        Product product = _dataContext.Products.Find(id) ?? throw new InvalidOperationException();
        return product;
    }

    public List<Product> GetProducts()
    {
        var products = _dataContext.Products.ToList();
     if(products!=null)
        return products;
     return new List<Product>();
    }
    }

    