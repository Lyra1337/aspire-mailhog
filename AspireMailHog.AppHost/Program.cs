var builder = DistributedApplication.CreateBuilder(args);

var mailServer = builder.AddContainer("mailhog", "mailhog/mailhog")
    .WithEndpoint(
        name: "mailhog-smtp",
        port: 1025,
        targetPort: 1025
    )
    .WithHttpEndpoint(
        name: "mailhog-web",
        port: 8025,
        targetPort: 8025
    );

var apiService = builder.AddProject<Projects.AspireMailHog_ApiService>("apiservice")
    .WithReference(mailServer.GetEndpoint("mailhog-smtp"))
    .WaitFor(mailServer);

builder.AddProject<Projects.AspireMailHog_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
