using ResumeManagementApplication.Shared.Models;

namespace ResumeManagementApplication.Shared.Abstractions
{
    public interface IValidator
    {
        bool IsValid(Candidate candidate);
        bool IsValid(Degree degree);
        bool IsValidFirstName(Candidate candidate);
        bool IsValidLastName(Candidate candidate);
        bool IsValidMobile(Candidate candidate);
        bool IsValidEmail(Candidate candidate);
        bool IsValidDegreeName(Degree degree);
    }
}