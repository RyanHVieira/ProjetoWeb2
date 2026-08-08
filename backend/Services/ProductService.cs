using backend.Data;
using backend.Models;

namespace backend.services.products;


public class ProdutoService{
    private readonly AppDbContext _context;
    public ProdutoService(AppDbContext context){
        _context = context;
    }
    
    public Product? GetProdutoById(int id){
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product CreateProduct(String name, decimal price, string description, string imageUrl){
        Product product = new Product{Name = name, Price = price, Description = description, ImageUrl = imageUrl, CreatedAt = DateTime.UtcNow};

        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public List<Product> GetAllProducts(){
        return _context.Products.ToList();
    }

    public void UpdateProduct(int id, String name, decimal price, string description, string imageUrl){
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

    public void DeleteProduct(int id){
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null){
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}