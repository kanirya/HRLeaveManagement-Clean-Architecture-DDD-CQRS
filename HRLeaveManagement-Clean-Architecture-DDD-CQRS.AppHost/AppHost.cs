var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache").WithRedisCommander();

builder.AddProject<Projects.API>("api").WithReference(cache);

builder.Build().Run();
