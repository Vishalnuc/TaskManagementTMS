using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var constr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TaskManagementDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

// Enable CORS with a policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Allow Angular frontend to make requests
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// Use the CORS middleware
app.UseCors("AllowLocalhost");
//app.UseHttpsRedirection();
app.MapControllers();
app.Run();
