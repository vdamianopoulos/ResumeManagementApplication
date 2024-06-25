using System.Text;
using System.Text.Json;
using ResumeManagementApplication.Shared.Abstractions;
using ResumeManagementApplication.Shared.Models;
using static ResumeManagementApplication.Shared.Validations.ModelsValidationLogic;

namespace ResumeManagementApplication.Shared.Services
{
    public class DegreeService : IDegreeService
    {
        private ILogger<DegreeService> _logger;
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _apiEndpoints;

        public DegreeService(
            ILogger<DegreeService> logger,
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiEndpoints = new Dictionary<string, string>()
            {
                { nameof(GetAllAsync), _httpClient.BaseAddress.OriginalString+"/degrees/getAll" },
                { nameof(GetByIdsAsync), _httpClient.BaseAddress.OriginalString+"/degrees/getByIds/{0}" },
                { nameof(DeleteByIdAsync), _httpClient.BaseAddress.OriginalString+"/degrees/deleteById/{0}" },
                { nameof(SaveAsync), _httpClient.BaseAddress.OriginalString+"/degrees/save" },
                { nameof(RemoveUnusedDegreesAsync), _httpClient.BaseAddress.OriginalString+"/degrees/removeUnusedDegrees" }
            }; ;
        }

        public async Task<List<Degree>> GetAllAsync()
        {
            var result = await SendRequestAsync<List<Degree>>(_apiEndpoints[nameof(GetAllAsync)], HttpMethod.Get);
            return result.ToList();
        }

        public async Task<List<Degree>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var result = await SendRequestAsync<List<Degree>>(string.Format(_apiEndpoints[nameof(GetByIdsAsync)], string.Join(',', ids)), HttpMethod.Get);
            return result.ToList();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await SendRequestAsync<bool>(string.Format(_apiEndpoints[nameof(DeleteByIdAsync)], id), HttpMethod.Delete);
        }

        public async Task<bool> SaveAsync(Degree degree)
        {
            return await SendRequestAsync<bool>(_apiEndpoints[nameof(SaveAsync)], HttpMethod.Post, degree);
        }

        public async Task<bool> RemoveUnusedDegreesAsync()
        {
            return await SendRequestAsync<bool>(_apiEndpoints[nameof(RemoveUnusedDegreesAsync)], HttpMethod.Delete);
        }

        private async Task<T> SendRequestAsync<T>(string url, HttpMethod method, Degree body = null) where T : new()
        {
            try
            {
                if (body is not null && !IsRequestBodyDataValid(body))
                    throw new Exception("Failed to send request: Invalid request data format.");

                var request = new HttpRequestMessage(method, url)
                {
                    Content =
                    body == null
                    ? null
                    : new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                    response.StatusCode == System.Net.HttpStatusCode.Created ||
                    response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new T();
                }
                else
                {
                    throw new Exception($"Failed to send request: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new T();
            }
        }

        private static bool IsRequestBodyDataValid(Degree degree)
        {
            var validations = new ModelValidator();
            return validations.IsValid(degree);
        }
    }
}
