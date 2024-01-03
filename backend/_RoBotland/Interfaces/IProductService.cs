﻿using _RoBotland.Enums;
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
        public List<ProductDto> GetFilteredProducts(ProductFilterDto filterParameters);
        int AddCategoryToProduct(int categoryId, int productId);
        public List<ProductDto> SearchProductsByName(string productName);
        ProductDto ChangeProductAvailability(Availability availability, ProductDto dto);
        List<CategoryDto> GetCategories();
    }
}
