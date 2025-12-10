using System.Security.Claims;
using Cscore.API.Authorization.Requirements;
using Cscore.API.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Cscore.API.Authorization.Handlers;

public class JudgeAuthorizationHandler : AuthorizationHandler<JudgeRequirement>
{
    private readonly IChampionshipRepository _championshipRepo;

    public JudgeAuthorizationHandler(IChampionshipRepository championshipRepo)
    {
        _championshipRepo = championshipRepo;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        JudgeRequirement requirement)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return;

        var userId = int.Parse(userIdClaim.Value);

        // Admin tem acesso total
        var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
        if (role == "Admin")
        {
            context.Succeed(requirement);
            return;
        }

        // Verificar se é juiz do campeonato
        var isJudge = await _championshipRepo.IsUserJudgeOfChampionship(
            userId,
            requirement.ChampionshipId
        );

        if (isJudge)
            context.Succeed(requirement);
    }
}
