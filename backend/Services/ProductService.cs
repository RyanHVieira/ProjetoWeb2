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

    public Product CreateProduct(string name, decimal price, string description, int quantity, string imageUrl){
        Product product = new Product{Id = Guid.NewGuid(),Name = name, Description = description, Price = price, Quantity = quantity, ImageUrl = imageUrl, CreatedAt = DateTime.UtcNow};
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public List<Product> GetAllProducts(){
        return _context.Products.ToList();
    }

    public void UpdateProduct(Guid id, string name, decimal price, string description, int quantity, string imageUrl){
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null){
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.Quantity = quantity;
            product.ImageUrl = imageUrl;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }

    public bool DeleteProduct(Guid id){
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return false;
        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }
}