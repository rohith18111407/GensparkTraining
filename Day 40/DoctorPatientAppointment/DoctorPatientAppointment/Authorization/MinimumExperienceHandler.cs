using Microsoft.AspNetCore.Authorization;

namespace DoctorPatientAppointment.Policies
{
    public class MinimumExperienceHandler : AuthorizationHandler<MinimumExperienceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MinimumExperienceRequirement requirement)
        {
            var experienceClaim = context.User.FindFirst("YearsOfExperience")?.Value; 
            if (experienceClaim != null &&
                int.TryParse(experienceClaim, out int years) &&
                years >= requirement.MinExperience)
            {
                context.Succeed(requirement);
            }
 
            return Task.CompletedTask;
        }
    }

}