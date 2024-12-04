var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AspireMailHog_ApiService>("apiservice");

builder.AddProject<Projects.AspireMailHog_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
