using DataPersistency.Abstractions;
using DataPersistency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DataPersistency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DegreesController : ControllerBase
    {
        private readonly JsonSerializerOptions _options;
        private readonly IDegreeService _degreeService;

        public DegreesController(IDegreeService degreeService)
        {
            _degreeService = degreeService;
            _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        /// <summary>
        /// Gets a list of all degrees.
        /// </summary>
        /// <returns>A collection of Degree objects.</returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var degrees = await _degreeService.GetAllAsync();
            return degrees.Any() ? Ok(JsonSerializer.Serialize(degrees, _options)) : NotFound();
        }

        /// <summary>
        /// Gets degrees by IDs.
        /// </summary>
        /// <param name="id">The comma seperated ids of the degrees to retrieve.</param>
        /// <returns>A collection of Degrees if Any, NotFound otherwise.</returns>
        [HttpGet("getByIds/{commaSeperatedIds}")]
        public async Task<IActionResult> GetByIdsAsync(string commaSeperatedIds)
        {
            var ids = commaSeperatedIds?.Split(',').SelectMany(id => int.TryParse(id, out int parsedId) ? new List<int>() { parsedId } : Enumerable.Empty<int>());
            if (ids == null || !ids.Any())
                return NotFound();

            var degree = await _degreeService.GetByIdsAsync(ids);
            return (degree != null) ? Ok(JsonSerializer.Serialize(degree, _options)) : NotFound();
        }

        /// <summary>
        /// Deletes a specific degree by ID.
        /// </summary>
        /// <param name="id">The ID of the degree to delete.</param>
        /// <returns>A boolean if found and deleted, NotFound otherwise.</returns>
        [HttpDelete("deleteById/{id}")]
        public async Task<ActionResult<bool>> DeleteByIdAsync(int id)
        {
            var success = await _degreeService.DeleteByIdAsync(id);
            return (success == true) ? Ok(JsonSerializer.Serialize(success, _options)) : NotFound();
        }

        /// <summary>
        /// Saves a specific degree.
        /// </summary>
        /// <param name="id">The degree to save.</param>
        /// <returns>A boolean if found and saved, NotFound otherwise.</returns>
        [HttpPost("save")]
        public async Task<ActionResult<bool>> SaveAsync(Degree degree)
        {
            var success = await _degreeService.SaveAsync(degree);
            return (success == true) ? Ok(JsonSerializer.Serialize(success, _options)) : NotFound();
        }

        /// <summary>
        /// Removes unused Degrees
        /// </summary>
        /// <returns>A boolean if found and deleted, NotFound otherwise.</returns>
        [HttpDelete("removeUnusedDegrees")]
        public async Task<ActionResult<bool>> RemoveUnusedDegreesAsync()
        {
            var success = await _degreeService.RemoveUnusedDegreesAsync();
            return (success == true) ? Ok(JsonSerializer.Serialize(success, _options)) : NotFound();
        }

    }
}
