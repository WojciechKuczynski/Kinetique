using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Kinetique_Main_API>("main");
builder.AddProject<Kinetique_Schedule>("schedule");
builder.AddProject<Kinetique_Appointment_API>("appointment");
builder.AddProject<Kinetique_Nfz_API>("nfz");


builder.Build().Run();