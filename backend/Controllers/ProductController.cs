using backend.services.products;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductController;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase{
    private readonly ProductService _productService;
    public ProductController(ProductService productService){
        _productService = productService;
    }

    [HttpPost("add")]
    public IActionResult AddProduct([FromBody] ProductCreateDTO request){
        var product = _productService.CreateProduct(request.Name,request.Price,request.Description,request.ImageUrl);

        return Ok(product);
    }
}