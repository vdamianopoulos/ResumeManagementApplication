using System.ComponentModel.DataAnnotations;

namespace DataPersistency.Models
{
    public class Candidate : IEntity
    {
        public Candidate() { }
        public Candidate(int id, string lastName, string firstName, string email, string mobile, int degreeId, byte[] cV, DateTime creationTime) : this(lastName, firstName, email, mobile, degreeId, cV)
        {
            Id = id;
            CreationTime = creationTime;
        }

        public Candidate(string lastName, string firstName, string email, string mobile, int degreeId, byte[] cV)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Mobile = mobile;
            DegreeId = degreeId;
            CV = cV;
            CreationTime = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public int DegreeId { get; set; }
        public byte[] CV { get; set; } = [];
        public DateTime CreationTime { get; set; }
    }
}
