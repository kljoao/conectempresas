var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
options.AddPolicy("AllowLocalHost", policy => {
    policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500", "localhost/:1", "http://localhost", "192.168.32.10", "http://192.168.32.10", "192.168.32.10:80", "http://192.168.32.10:80", "http://192.168.32.10:85", "192.168.32.10:85")
        .SetIsOriginAllowed(_ => true)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


// Add services to the container.

builder.Services.AddControllers();
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

app.UseRouting();

app.UseCors("AllowLocalHost");

app.UseAuthorization();

app.MapControllers();

app.Run();
