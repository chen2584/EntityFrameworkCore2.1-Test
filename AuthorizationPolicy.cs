using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

public class TestPolicy : AuthorizationHandler<TestPolicy>, IAuthorizationRequirement
{

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestPolicy requirement)
    {
        //context.Succeed(requirement);
        context.Fail();
        return Task.FromResult(false);
    }
}

