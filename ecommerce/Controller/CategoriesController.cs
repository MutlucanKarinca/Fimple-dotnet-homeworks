using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce;

[Route("/api/v1/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private static List<Category> _categories = new List<Category>()
    {
        new Category { Id = 1, Name = "Laptop" },
        new Category { Id = 2, Name = "Smartphone" },
        new Category { Id = 3, Name = "Small Appliances" }
    };


    [HttpGet]
    public IActionResult GetAll([FromQuery] FilteringParameters filteringParameters)
    {
        var filteredData = _categories.AsQueryable();

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
        var result = filteredData.ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var category = _categories.SingleOrDefault(p => p.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Category category)
    {
        if (category == null)
        {
            return BadRequest();
        }

        _categories.Add(category);

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Category category)
    {
        if (category == null || id != category.Id)
        {
            return BadRequest();
        }

        Category categoryToUpdate = _categories.SingleOrDefault(p => p.Id == category.Id);

        if (categoryToUpdate == null)
        {
            return NotFound();
        }

        categoryToUpdate.Name = category.Name;

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Category categoryToDelete = _categories.SingleOrDefault(p => p.Id == id);

        if (categoryToDelete == null)
        {
            return NotFound();
        }

        _categories.Remove(categoryToDelete);


        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Category> patchDocument)
    {
        if (patchDocument == null)
        {
            return BadRequest();
        }
    
        var category = _categories.SingleOrDefault(p => p.Id == id);
    
        if (category == null)
        {
            return NotFound();
        }
    
        patchDocument.ApplyTo(category, ModelState);
    
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        return Ok();
    }
}