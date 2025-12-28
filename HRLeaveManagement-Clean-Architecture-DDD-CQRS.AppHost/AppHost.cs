using Aspire.Hosting;

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


// AWS service with terraform 

var localstack = builder.AddContainer("localstack", "localstack/localstack")
    .WithEnvironment("SERVICES", "logs")
    .WithEnvironment("AWS_DEFAULT_REGION", "us-east-1")
    .WithEndpoint(4566, 4566);

var terraform = builder.AddExecutable(
        "terraform",
        "terraform",
        "../infra/terraform"
    )
    .WithArgs("init")
    .WithArgs("apply", "-auto-approve")
    .WithEnvironment("AWS_ACCESS_KEY_ID", "test")
    .WithEnvironment("AWS_SECRET_ACCESS_KEY", "test")
    .WithEnvironment("AWS_DEFAULT_REGION", "us-east-1")
    .WaitFor(localstack);

var otel = builder.AddContainer("otel-collector", "otel/opentelemetry-collector")
    .WithBindMount("../otel/otel-collector.yaml", "/etc/otel/config.yaml")
    .WithArgs("--config=/etc/otel/config.yaml");

builder.AddProject<Projects.API>("api")
    .WithEnvironment("OTEL_EXPORTER_OTLP_ENDPOINT", otel.GetEndpoint("otlp"))
    .WaitFor(otel)
    .WaitFor(localstack)
    .WaitFor(terraform)
    .WithReference(cache).WithReference(sql);



builder.Build().Run();
