using System.Text.Json.Serialization;
using static ResumeManagementApplication.Shared.Validations.ModelsValidationLogic;

namespace ResumeManagementApplication.Shared.Models
{
    public class Degree : IEntity
    {
        public Degree() { }
        [JsonConstructor]
        public Degree(int id, string name, DateTime creationTime)
        {
            Id = id;
            Name = name;
            CreationTime = creationTime;
        }

        public int Id { get; set; }

        [DegreeNameValidation]
        public string Name { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
