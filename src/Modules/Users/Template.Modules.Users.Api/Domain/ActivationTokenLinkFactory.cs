using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Template.Modules.Users.Api.Domain.Tokens;

public sealed class ActivationTokenLinkFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    public ActivationTokenLinkFactory(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }

    public string CreateLink(ActivationToken activationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
            throw new InvalidOperationException("Cannot create activation link when HttpContext is not available.");

        var activationLink = _linkGenerator.GetUriByName(
            httpContext,
            "",
            values: new { });

        if (string.IsNullOrEmpty(activationLink))
            throw new InvalidOperationException("Failed to generate activation link.");

        return activationLink;
    }
}