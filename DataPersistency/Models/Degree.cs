using DataPersistency.Extentions;
using System.ComponentModel.DataAnnotations;

namespace DataPersistency.Models
{
    public class Degree : IEntity
    {
        public Degree() { }

        public Degree(int id, string name, DateTime creationTime)
        {
            Id = id;
            Name = name;
            CreationTime = creationTime;
        }

        public Degree(string name)
        {
            Name = name;
            CreationTime = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public enum SeedDataDegrees
    {
        [EnumDescription("None")]
        None = 0,
        [EnumDescription("Associate Degree")]
        AssociateDegree = 1,
        [EnumDescription("Bachelor Of Arts")]
        BachelorOfArts = 2,
        [EnumDescription("Bachelor Of Science")]
        BachelorOfScience = 3,
        [EnumDescription("Bachelor Of Engineering")]
        BachelorOfEngineering = 4,
        [EnumDescription("Master Of Arts")]
        MasterOfArts = 5,
        [EnumDescription("Master Of Science")]
        MasterOfScience = 6,
        [EnumDescription("Master Of Business Administration")]
        MasterOfBusinessAdministration = 7,
        [EnumDescription("Doctor Of Philosophy")]
        DoctorOfPhilosophy = 8,
        [EnumDescription("Doctor Of Medicine")]
        DoctorOfMedicine = 9,
        [EnumDescription("Juris Doctor")]
        JurisDoctor = 10,
        [EnumDescription("Doctor Of Education")]
        DoctorOfEducation = 11
    }
}
