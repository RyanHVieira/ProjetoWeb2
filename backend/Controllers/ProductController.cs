using backend.services.products;
using Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductController;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase{
    private readonly ProductService _productService;
    public ProductController(ProductService productService){
        _productService = productService;
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddProduct([FromBody] ProductCreateDTO request){
        var product = _productService.CreateProduct(request.Name, request.Price, request.Description, request.Quantity, request.ImageUrl);

        return StatusCode(201, product);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(Guid id, [FromBody] ProductUpdateDTO request){
        _productService.UpdateProduct(id, request.Name, request.Price, request.Description, request.Quantity, request.ImageUrl);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(Guid id){
        var result = _productService.DeleteProduct(id);
        if (!result) return NotFound();
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(Guid id){
        var product = _productService.GetProdutoById(id);
        if(product == null){
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet]
    public IActionResult GetAllProducts(){
        var products = _productService.GetAllProducts();
        return Ok(products);
    }
}