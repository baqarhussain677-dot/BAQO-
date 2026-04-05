var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// --- 1. CORS Service Add karein ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Kisi bhi frontend se allow karein
              .AllowAnyMethod()   // GET, POST sab allow karein
              .AllowAnyHeader();  // Headers allow karein
    });
});
// ----------------------------------

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

app.UseHttpsRedirection();

// --- 2. CORS ko Activate karein (Zaroori Step) ---
// Note: Isko 'UseAuthorization' se PEHLE likhna zaroori hai
app.UseCors("AllowAll");
// -----------------------------------------------

app.UseAuthorization();

app.MapControllers();

app.Run();