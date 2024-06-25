using System.Text.Json.Serialization;

namespace ResumeManagementApplication.Shared.Models
{
    public class Candidate : IEntity
    {
        public Candidate() { }

        [JsonConstructor]
        public Candidate(int id, string lastName, string firstName, string email, string mobile, int degreeId, byte[] cV, DateTime creationTime)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Mobile = mobile;
            DegreeId = degreeId;
            CV = cV;
            CreationTime = creationTime;
        }

        public int Id { get; set; }

        //[NameValidation]
        public string LastName { get; set; } = string.Empty;

        //[NameValidation]
        public string FirstName { get; set; } = string.Empty;

        //There is already validation by the form
        public string Email { get; set; } = string.Empty;

        //[MobileValidation]
        public string Mobile { get; set; } = string.Empty;

        public int DegreeId { get; set; }
        public byte[] CV { get; set; } = [];
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
