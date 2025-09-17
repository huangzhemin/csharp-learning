using Microsoft.OpenApi.Models;
using WebAPIDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "C# 学习 Web API",
        Version = "v1",
        Description = "这是一个用于学习C# Web开发的示例API",
        Contact = new OpenApiContact
        {
            Name = "C# 学习者",
            Email = "learner@example.com"
        }
    });
});

// 添加CORS支持
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 添加日志
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "C# 学习 Web API v1");
        c.RoutePrefix = string.Empty; // 设置Swagger UI为根路径
    });
}

app.UseHttpsRedirection();

// 启用CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// 添加自定义端点
app.MapGet("/", () => "欢迎使用 C# 学习 Web API！访问 /swagger 查看API文档。");

app.MapGet("/api/health", () => new { Status = "Healthy", Timestamp = DateTime.UtcNow });

app.Run();
