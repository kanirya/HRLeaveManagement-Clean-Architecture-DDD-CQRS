var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache").WithRedisCommander();
//var sqlServer = builder.AddSqlServer("db", options =>
//{
//    options.Database = "AspireDb";         // DB name
//    options.Username = "sa";               // user
//    options.Password = "Strong!Pass123";   // password
//    options.Port = 1433;                   // optional, default SQL port
//});

builder.AddProject<Projects.API>("api")
    .WithReference(cache)
    //.WithReference(sqlServer)
    ;

builder.Build().Run();
