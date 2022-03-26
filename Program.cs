using librawry.portable;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cons = builder.Configuration.GetConnectionString("SqliteDatabase");

builder.Services.AddControllers();
builder.Services.AddDbContext<LibrawryContext>(options => options.UseSqlite(cons));

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
