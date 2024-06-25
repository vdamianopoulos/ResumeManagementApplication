using DataPersistency.Abstractions;
using DataPersistency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DataPersistency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly JsonSerializerOptions _options;
        private readonly ICandidateService _candidateService;
        private readonly ICandidateSpecificOperationsRepository _candidateSpecificOperationsRepository;

        public CandidatesController(ICandidateService candidateService, ICandidateSpecificOperationsRepository candidateSpecificOperationsRepository)
        {
            _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _candidateService = candidateService;
            _candidateSpecificOperationsRepository = candidateSpecificOperationsRepository;
        }

        /// <summary>
        /// Gets a list of all candidates.
        /// </summary>
        /// <returns>A collection of Candidate objects.</returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var candidates = await _candidateService.GetAllAsync();
            return candidates.Any() ? Ok(JsonSerializer.Serialize(candidates, _options)) : NotFound();
        }

        //// <summary>
        /// Gets candidates by IDs.
        /// </summary>
        /// <param name="id">The IDs of the candidates to retrieve.</param>
        /// <returns>A collection of Candidates if Any, NotFound otherwise.</returns>
        [HttpGet("getByIds/{commaSeperatedIds}")]
        public async Task<ActionResult<Candidate>> GetByIdsAsync(string commaSeperatedIds)
        {
            var ids = commaSeperatedIds?.Split(',').SelectMany(id => int.TryParse(id, out int parsedId) ? new List<int>() { parsedId } : Enumerable.Empty<int>());
            if (ids == null || !ids.Any())
                return NotFound();

            var candidate = await _candidateService.GetByIdsAsync(ids);
            return (candidate != null) ? Ok(JsonSerializer.Serialize(candidate, _options)) : NotFound();
        }

        /// <summary>
        /// Deletes a specific candidate by ID.
        /// </summary>
        /// <param name="id">The ID of the candidate to delete.</param>
        /// <returns>A boolean if found and deleted, NotFound otherwise.</returns>
        [HttpDelete("deleteById/{id}")]
        public async Task<ActionResult<bool>> DeleteByIdAsync(int id)
        {
            var success = await _candidateService.DeleteByIdAsync(id);
            return (success == true) ? Ok(JsonSerializer.Serialize(success, _options)) : NotFound();
        }

        /// <summary>
        /// Saves a specific candidate.
        /// </summary>
        /// <param name="id">The candidate to save.</param>
        /// <returns>A boolean if found and saved, NotFound otherwise.</returns>
        [HttpPost("save")]
        public async Task<ActionResult<bool>> SaveAsync(Candidate candidate)
        {
            var success = await _candidateService.SaveAsync(candidate);
            return (success == true) ? Ok(JsonSerializer.Serialize(success, _options)) : NotFound();
        }
    }
}
