using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ResumeManagementApplication.Shared.Abstractions;
using ResumeManagementApplication.Shared.Services;
using static ResumeManagementApplication.Shared.Validations.ModelsValidationLogic;

namespace ResumeManagementApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7234") });

            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped<IDegreeService, DegreeService>();

            builder.Services.AddSingleton<IValidator, ModelValidator>();

            await builder.Build().RunAsync();
        }
    }
}
