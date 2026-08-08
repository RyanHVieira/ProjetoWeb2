using backend.Data;
using backend.Models;

namespace backend.services.products;


public class ProductService{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context){
        _context = context;
    }
    
    public Product? GetProdutoById(Guid id){
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product CreateProduct(String name, decimal price, string description, int quantity, string imageUrl){
        Product product = new Product{Name = name,Description = description, Price = price, Quantity = quantity, ImageUrl = imageUrl, CreatedAt = DateTime.UtcNow};

        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public List<Product> GetAllProducts(){
        return _context.Products.ToList();
    }

    public void UpdateProduct(Guid id, String name, decimal price, string description, string imageUrl){
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null){
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.ImageUrl = imageUrl;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }

    public void DeleteProduct(Guid id){
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null){
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}