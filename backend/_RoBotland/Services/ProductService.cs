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
        var product = _mapper.Map<ProductDto>(dto);
        _dataContext.Products.Add(product);
        _dataContext.SaveChanges();
        var productDto= _mapper.Map<ProductDto>(dto);
        return productDto.Id;
    }

    public void DeleteProduct(int id)
    {
        ProductDto product = _dataContext.Products.Find(id) ?? throw new Exception();
        _dataContext.Remove(product);
        _dataContext.SaveChanges();
    }

    public int UpdateProduct(int id, ProductDto product)
    {

        var oldProduct = _dataContext.Products.Find(id) ?? throw new Exception();
        _dataContext.Products.Update(product);
        return oldProduct.Id;
    }

    public ProductDto GetProductById(int id)
    {
        ProductDto product = _dataContext.Products.Find(id) ?? throw new Exception("Not Found at "+id);
        return product;
    }

    public List<ProductDto> GetProducts()
    {
        var products = _dataContext.Products.ToList();
        return products;
    }


    public List<ProductDto> GetFilteredProducts(ProductFilterDto filterParameters)
    {
        var query = _dataContext.Products.AsQueryable();

        if (filterParameters.MinPrice.HasValue)
         query = query.Where(p => p.Price >= filterParameters.MinPrice);

        if (filterParameters.MaxPrice.HasValue)
           query = query.Where(p => p.Price <= filterParameters.MaxPrice);

       

        //if (filterParameters.CategoryId.HasValue)
        //query = query.Where(p => p.Categories.Any(c => c.Id == filterParameters.CategoryId.Value));

        var products = query.ToList();
        return _mapper.Map<List<ProductDto>>(products);
    }

}

    