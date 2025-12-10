using Microsoft.AspNetCore.Authorization;

namespace Cscore.API.Authorization.Requirements;

public class JudgeRequirement : IAuthorizationRequirement
{
    public int ChampionshipId { get; set; }

    public JudgeRequirement(int championshipId)
    {
        ChampionshipId = championshipId;
    }
}
