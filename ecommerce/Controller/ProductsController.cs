using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce;

[Route("/api/v1/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>()
    {
        new Product { Id = 1, Name = "Laptop", CategoryId = 1, QuantityPerUnit = "1 unit", UnitPrice = 999.99m,
            UnitsInStock = 50 },
        new Product { Id = 2, Name = "Smartphone", CategoryId = 2, QuantityPerUnit = "1 unit", UnitPrice = 599.99m,
            UnitsInStock = 100 },
        new Product { Id = 4, Name = "Coffee Maker", CategoryId = 3, QuantityPerUnit = "1 unit", UnitPrice = 49.99m,
            UnitsInStock = 75 }
    };

    [HttpGet]
    public IActionResult GetAll([FromQuery] FilteringParameters filteringParameters,
        [FromQuery] PagingParameters pagingParameters, [FromQuery] SortingParameters sortingParameters)
    {
        var filteredData = _products.AsQueryable();

        if (filteringParameters != null)
        {
            if (filteringParameters.CategoryId.HasValue)
            {
                filteredData = filteredData.Where(x => x.Id == filteringParameters.CategoryId);
            }

            if (filteringParameters.Name!=null)
            {
                filteredData = filteredData.Where(x => x.Name.ToLower() == filteringParameters.Name.ToLower());
            }
        }

        if (sortingParameters != null)
        {
            if (!string.IsNullOrEmpty(sortingParameters.OrderBy))
            {
                filteredData = sortingParameters.OrderBy.ToLower() switch
                {
                    "unitprice" => sortingParameters.SortOrder == "asc"
                        ? filteredData.OrderBy(x => x.UnitPrice)
                        : filteredData.OrderByDescending(x => x.UnitPrice)
                    , // Varsayılan sıralama
                    "unitsinstock" => sortingParameters.SortOrder == "asc"
                        ? filteredData.OrderBy(x => x.UnitsInStock)
                        : filteredData.OrderByDescending(x => x.UnitsInStock),
                    _ => filteredData, // Varsayılan sıralama
                };
            }
        }

        if (pagingParameters != null)
        {
            var page = pagingParameters.PageNumber != null ? pagingParameters.PageNumber : 1;
            var size = pagingParameters.PageSize != null ? pagingParameters.PageSize : 10;


            filteredData = filteredData.Skip((page - 1) * size).Take(size);
        }

        var result = filteredData.ToList();

        return Ok(result);
    }


    // [HttpGet]
    // public IActionResult GetAll()
    // {
    //     var result = _products.OrderBy(x=>x.Date).ToList<ProductProduct>();
    //
    //     return Ok(result);
    // }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var produdct = _products.SingleOrDefault(p => p.Id == id);

        if (produdct == null)
        {
            return NotFound();
        }

        return Ok(produdct);
    }

    [HttpGet("spaceObjectsId/{spaceObjectId}")]
    public IActionResult GetBySpaceObjectId(int spaceObjectId)
    {
        var produdct = _products.SingleOrDefault(p => p.Id == spaceObjectId);

        if (produdct == null)
        {
            return NotFound();
        }

        return Ok(produdct);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        _products.Add(product);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Product product)
    {
        if (product == null || id != product.Id)
        {
            return BadRequest();
        }

        Product productToUpdate = _products.SingleOrDefault(p => p.Id == product.Id);

        if (productToUpdate == null)
        {
            return NotFound();
        }

        productToUpdate.Name = product.Name;
        productToUpdate.CategoryId = product.CategoryId;
        productToUpdate.QuantityPerUnit = product.QuantityPerUnit;
        productToUpdate.UnitsInStock = product.UnitsInStock;
        productToUpdate.UnitPrice = product.UnitPrice;

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Product productToDelete = _products.SingleOrDefault(p => p.Id == id);

        if (productToDelete == null)
        {
            return NotFound();
        }

        _products.Remove(productToDelete);


        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
    {
        if (patchDocument == null)
        {
            return BadRequest();
        }
    
        var produdct = _products.SingleOrDefault(p => p.Id == id);
    
        if (produdct == null)
        {
            return NotFound();
        }
    
        patchDocument.ApplyTo(produdct, ModelState);
    
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        return Ok();
    }
}