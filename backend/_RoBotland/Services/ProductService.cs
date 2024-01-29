using _RoBotland.Enums;
using _RoBotland.Interfaces;
using _RoBotland.Migrations;
using _RoBotland.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _RoBotland.Services;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public ProductService(DataContext dataContext, IMapper mapper )
    {
        this._dataContext=dataContext;
        this._mapper=mapper;
    }
    
    public int AddNewProduct(ProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        _dataContext.Products.Add(product);
        _dataContext.SaveChanges();
        var productDto= _mapper.Map<ProductDto>(product);
        return productDto.Id;
    }

    public void DeleteProduct(int id)
    {
        Product product = _dataContext.Products.Find(id) ?? throw new Exception();
        _dataContext.Remove(product);
        _dataContext.SaveChanges();
    }

    public int UpdateProduct(int id, ProductDto product)
    {
        var oldProduct = _dataContext.Products.Find(id) ?? throw new Exception();
        var nproduct = _mapper.Map<Product>(product);
        nproduct.Id = id;
        _dataContext.Entry(oldProduct).CurrentValues.SetValues(nproduct);
        _dataContext.SaveChanges();
        return nproduct.Id;
    }

    public ProductDto GetProductById(int id)
    {
        Product product = _dataContext.Products.Find(id) ?? throw new Exception("Not Found at " + id);
        var getProduct = _mapper.Map<ProductDto>(product);
        return getProduct;
    }

    public List<CategoryDto> GetCategories()
    {
        var categories = _dataContext.Categories
            .Select(x=>_mapper.Map<CategoryDto>(x))
            .ToList();
        return categories;
    }
    public List<ProductDto> GetProducts()
    {
        var products = _dataContext.Products.Select(x=>_mapper.Map<ProductDto>(x)).ToList();
        return products;
    }
    public List<ProductDto> SearchProductsByName(string productName)
    {
        var query = _dataContext.Products.AsQueryable();
        if (!string.IsNullOrEmpty(productName))
        {
            var upperProductName = productName.ToUpper();
            query = query.Where(p => p.Name.ToUpper().Contains(upperProductName));
        }
        var products = query.ToList();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public List<ProductDto> GetFilteredProducts(ProductFilterDto filterParameters)
    {
        var query = _dataContext.Products.AsQueryable();
        if (!string.IsNullOrEmpty(filterParameters.ProductName))
        {
            var upperProductName = filterParameters.ProductName.ToUpper();
            query = query.Where(p => p.Name.ToUpper().Contains(upperProductName));
        }
        if (filterParameters.MinPrice.HasValue)
         query = query.Where(p => p.Price >= filterParameters.MinPrice);
        if (filterParameters.MaxPrice.HasValue)
           query = query.Where(p => p.Price <= filterParameters.MaxPrice);
        if (filterParameters.CategoryId.HasValue)
            query = query.Where(p => p.Categories.Any(c => c.Id == filterParameters.CategoryId.Value));
        query = query.Where(p => p.IsAvailable != Availability.D);
        if (filterParameters.IsAvailable.HasValue)
            query = query.Where(p => p.IsAvailable == filterParameters.IsAvailable.Value);
        var products = query.ToList();
        return _mapper.Map<List<ProductDto>>(products);
    }
    public ICollection<Category> AddCategoryToProduct(List<string> categoryNames, int productId)
    {
        using (var transaction = _dataContext.Database.BeginTransaction())
        {
            try
            {
                Product product = _dataContext.Products
                    .Include(p => p.Categories)
                    .FirstOrDefault(p => p.Id == productId) ?? throw new Exception("Product Not Exist");
                product.Categories.Clear();
                foreach (var categoryName in categoryNames)
                {
                    Category category = _dataContext.Categories.FirstOrDefault(c => c.Name == categoryName);
                    if (category == null)
                    {
                        category = new Category { Name = categoryName };
                        _dataContext.Categories.Add(category);
                    }
                    product.Categories.Add(category);
                }
                _dataContext.SaveChanges();
                transaction.Commit();
                var categories = product.Categories.ToList();
                return categories;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error during category assignment", ex);
            }
        }
    }
    public ProductDto ChangeProductAvailability(Availability availability,int id)
    {
        var product = _dataContext.Products.Find(id) ?? throw new Exception("Product doesn't exist");
        product.IsAvailable = availability;
        _dataContext.SaveChanges();
        var productToReturn = _mapper.Map<ProductDto>(product);
        return productToReturn;
    }

    public List<Category> GetProductCategories(int productId)
    {
        var categories = _dataContext.Products
             .Where(p => p.Id == productId)
             .SelectMany(p => p.Categories)
             .ToList();
        return categories;
    }
}

    