using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();
builder.Services.Configure<MongoDBUserSettings>(builder.Configuration.GetSection("RegistrationService"));
builder.Services.AddSingleton<MongoDBUserService>();
builder.Services.Configure<MongoDBTasksSettings>(builder.Configuration.GetSection("TaskService"));
builder.Services.AddSingleton<MongoDBTaskService>();
builder.Services.Configure<JWTSecret>(builder.Configuration.GetSection("SecretKey"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ApiCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
