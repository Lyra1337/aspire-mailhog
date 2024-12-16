namespace Aspire.Hosting.ApplicationModel;

// For ease of discovery, resource types should be placed in
// the Aspire.Hosting.ApplicationModel namespace. If there is
// likelihood of a conflict on the resource name consider using
// an alternative namespace.
public sealed class MailHogResource(string name) : ContainerResource(name), IResourceWithConnectionString
{
    // Constants used to refer to well known-endpoint names, this is specific
    // for each resource type. MailHog exposes an SMTP endpoint and a HTTP
    // endpoint.
    internal const string SmtpEndpointName = "mailhog-smtp";
    internal const string HttpEndpointName = "mailhog-http";

    // An EndpointReference is a core .NET Aspire type used for keeping
    // track of endpoint details in expressions. Simple literal values cannot
    // be used because endpoints are not known until containers are launched.
    private EndpointReference? smtpReference;

    public EndpointReference SmtpEndpoint => this.smtpReference ??= new(this, SmtpEndpointName);

    // Required property on IResourceWithConnectionString. Represents a connection
    // string that applications can use to access the MailHog server. In this case
    // the connection string is composed of the SmtpEndpoint endpoint reference.
    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create(
            handler: $"smtp://{this.SmtpEndpoint.Property(EndpointProperty.Host)}:{this.SmtpEndpoint.Property(EndpointProperty.Port)}"
        );
}
