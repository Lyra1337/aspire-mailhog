var builder = DistributedApplication.CreateBuilder(args);

var mailHog = builder.AddMailHog("mailhog");

var apiService = builder.AddProject<Projects.AspireMailHog_ApiService>("apiservice")
    .WithReference(mailHog)
    .WaitFor(mailHog);

builder.AddProject<Projects.AspireMailHog_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
