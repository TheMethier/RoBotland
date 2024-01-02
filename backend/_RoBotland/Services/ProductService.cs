using _RoBotland.Enums;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

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
        var productDto= _mapper.Map<ProductDto>(dto);
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

        var oldProduct = _dataContext.Products.Find(id) ?? throw new Exception("Not Exist");
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

    public List<ProductDto> GetProducts()
    {
        var products = _dataContext.Products.ToList();
        List<ProductDto> productList = new List<ProductDto>();
        products.ForEach(x =>
        {
            if (x != null)
            {
                var productDto = _mapper.Map<ProductDto>(x);
                productList.Add(productDto);
            }
        });
        return productList;
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
        if (filterParameters.MinPrice.HasValue)
         query = query.Where(p => p.Price >= filterParameters.MinPrice);
        if (filterParameters.MaxPrice.HasValue)
           query = query.Where(p => p.Price <= filterParameters.MaxPrice);
        if (filterParameters.CategoryId.HasValue)
            query = query.Where(p => p.Categories.Any(c => c.Id == filterParameters.CategoryId.Value));
        if (filterParameters.IsAvailable.HasValue)
            query = query.Where(p => p.IsAvailable == filterParameters.IsAvailable.Value);
        var products = query.ToList();
        return _mapper.Map<List<ProductDto>>(products);
    }
    public int AddCategoryToProduct(int categoryId, int productId)
    {

        Category categoryToAddTo = _dataContext.Categories.Find(categoryId) ?? throw new Exception("Category Not Exist");
        Product productToAdd = _dataContext.Products.Find(productId) ?? throw new Exception("Product Not Exist");
        categoryToAddTo.Products.Add(productToAdd);
        _dataContext.SaveChanges();
        return categoryId;
    }

    public ProductDto ChangeProductAvailability(Availability availability, ProductDto dto)
    {
        var product = _dataContext.Products.Find(dto.Id) ?? throw new Exception("Product Not Exist");
        product.IsAvailable = availability;
        _dataContext.SaveChanges();
        var productToReturn = _mapper.Map<ProductDto>(product);
        return productToReturn;
    }

}

    