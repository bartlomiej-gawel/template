namespace Template.AppHost.Zitadel;

internal sealed class ZitadelResource : ContainerResource, IResourceWithConnectionString
{
    public ZitadelResource(string name)
        : base(name)
    {
    }

    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create($"http://{{{Name}.bindings.http.host}}:{{{Name}.bindings.http.port}}");
}