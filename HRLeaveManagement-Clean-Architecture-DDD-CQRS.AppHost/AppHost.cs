var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache").WithRedisCommander();
//var sqlServer = builder.AddSqlServer("db", options =>
//{
//    options.Database = "AspireDb";         // DB name
//    options.Username = "sa";               // user
//    options.Password = "Strong!Pass123";   // password
//    options.Port = 1433;                   // optional, default SQL port
//});
var sql = builder.AddSqlServer("DESKTOP-DFLU7SE").AddDatabase("LeaveManagement");


builder.AddProject<Projects.API>("api")
    .WithReference(cache).WithReference(sql);



builder.Build().Run();
