using librawry.portable.ef;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cons = builder.Configuration.GetConnectionString("SqliteDatabase");

builder.Services.AddControllers();
builder.Services.AddDbContext<LibrawryContext>(options => options.UseSqlite(cons));

var app = builder.Build();

if (app.Environment.IsProduction()) {
	app.UseHttpsRedirection();
}

if (app.Environment.IsDevelopment()) {
	app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();
