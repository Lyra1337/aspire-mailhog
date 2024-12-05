﻿// Put extensions in the Aspire.Hosting namespace to ease discovery as referencing
// the .NET Aspire hosting package automatically adds this namespace.
namespace Aspire.Hosting;

// This class just contains constant strings that can be updated periodically
// when new versions of the underlying container are released.
internal static class MailHogContainerImageTags
{
    internal const string Registry = "docker.io";

    internal const string Image = "mailhog/mailhog";

    internal const string Tag = "latest";
}