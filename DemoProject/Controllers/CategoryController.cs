using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly string _filePath = "Data/categories.json";

        private List<Category> LoadCategories()
        {
            if (!System.IO.File.Exists(_filePath))
                return new List<Category>();

            var json = System.IO.File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Category>>(json) ?? new List<Category>();
        }

        private void SaveCategories(List<Category> categories)
        {
            var json = JsonSerializer.Serialize(categories, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_filePath, json);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = LoadCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var categories = LoadCategories();
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            var categories = LoadCategories();
            category.Id = Guid.NewGuid();
            categories.Add(category);
            SaveCategories(categories);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Category updatedCategory)
        {
            var categories = LoadCategories();
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            category.Title = updatedCategory.Title;
            category.Code = updatedCategory.Code;
            category.Description = updatedCategory.Description;
            SaveCategories(categories);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var categories = LoadCategories();
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            categories.Remove(category);
            SaveCategories(categories);

            return NoContent();
        }
    }
}