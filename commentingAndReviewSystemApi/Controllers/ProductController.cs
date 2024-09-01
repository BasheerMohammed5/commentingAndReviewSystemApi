// In commentingAndReviewSystemApi/Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewManagementSystem.Infrastructure.Data;
using commentingAndReviewSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PATCH: api/products/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(product, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        if (!TryValidateModel(product))
        {
            return BadRequest(ModelState);
        }

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
