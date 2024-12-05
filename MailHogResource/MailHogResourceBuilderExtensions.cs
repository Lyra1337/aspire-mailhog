using Aspire.Hosting.ApplicationModel;

// Put extensions in the Aspire.Hosting namespace to ease discovery as referencing
// the .NET Aspire hosting package automatically adds this namespace.
namespace Aspire.Hosting;

public static class MailHogResourceBuilderExtensions
{
    /// <summary>
    /// Adds the <see cref="MailHogResource"/> to the given
    /// <paramref name="builder"/> instance. Uses the "latest" tag.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="httpPort">The HTTP port.</param>
    /// <param name="smtpPort">The SMTP port.</param>
    /// <returns>
    /// An <see cref="IResourceBuilder{MailHogResource}"/> instance that
    /// represents the added MailHog resource.
    /// </returns>
    public static IResourceBuilder<MailHogResource> AddMailHog(
        this IDistributedApplicationBuilder builder,
        string name,
        int? httpPort = null,
        int? smtpPort = null)
    {
        // The AddResource method is a core API within .NET Aspire and is
        // used by resource developers to wrap a custom resource in an
        // IResourceBuilder<T> instance. Extension methods to customize
        // the resource (if any exist) target the builder interface.
        var resource = new MailHogResource(name);

        return builder.AddResource(resource)
                      .WithImage(MailHogContainerImageTags.Image)
                      .WithImageRegistry(MailHogContainerImageTags.Registry)
                      .WithImageTag(MailHogContainerImageTags.Tag)
                      .WithHttpEndpoint(
                          targetPort: 8025,
                          port: httpPort,
                          name: MailHogResource.HttpEndpointName)
                      .WithEndpoint(
                          targetPort: 1025,
                          port: smtpPort,
                          name: MailHogResource.SmtpEndpointName);
    }
}
