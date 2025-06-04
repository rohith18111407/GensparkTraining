using Microsoft.AspNetCore.Authorization;

namespace DoctorPatientAppointment.Policies
{
    public class MinimumExperienceRequirement : IAuthorizationRequirement
{
    public int MinExperience { get; }
    public MinimumExperienceRequirement(int yearsRequired)
    {
        MinExperience = yearsRequired;
    }
}
}