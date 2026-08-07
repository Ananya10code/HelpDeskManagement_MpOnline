using HelpDesk.Api.Data;
using HelpDesk.Api.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Check if SQL Server LocalDB is accessible; otherwise use local SQLite database
bool useSqlServer = false;
if (!string.IsNullOrEmpty(connectionString))
{
    try
    {
        using var testConn = new SqlConnection(connectionString);
        testConn.Open();
        useSqlServer = true;
    }
    catch
    {
        useSqlServer = false;
    }
}

if (useSqlServer)
{
    builder.Services.AddDbContext<HelpDeskDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Use lightweight SQLite database if SQL Server LocalDB is not installed
    builder.Services.AddDbContext<HelpDeskDbContext>(options =>
        options.UseSqlite("Data Source=helpdesk.db"));
}

// Register Repository Pattern
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Ensure Database Created & Seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HelpDeskDbContext>();
    context.Database.EnsureCreated();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
