using DataPersistency.Models;
using System.Reflection;

namespace DataPersistency.Extentions
{
    public class EnumDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public EnumDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attribute = fieldInfo.GetCustomAttributes<EnumDescriptionAttribute>(false).FirstOrDefault();
            return attribute?.Description ?? "";
        }
        public static Candidate? UpdateValues(this Candidate? existingCandidateDetails, Candidate newCandidateDetails)
        {
            if (existingCandidateDetails == null)
                return existingCandidateDetails;

            existingCandidateDetails.FirstName = newCandidateDetails.FirstName;
            existingCandidateDetails.LastName = newCandidateDetails.LastName;
            existingCandidateDetails.Email = newCandidateDetails.Email;
            existingCandidateDetails.Mobile = newCandidateDetails.Mobile;
            existingCandidateDetails.CV = newCandidateDetails.CV;

            existingCandidateDetails.DegreeId = newCandidateDetails.DegreeId;

            return existingCandidateDetails;
        }
        public static Degree? UpdateValues(this Degree? existingDegreeDetails, Degree newDegreeDetails)
        {
            if (existingDegreeDetails == null)
                return existingDegreeDetails;

            existingDegreeDetails.Name = newDegreeDetails.Name;

            return existingDegreeDetails;
        }
    }
}
