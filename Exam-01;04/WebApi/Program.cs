using Infrastructure.Data;
using Infrastructure.Interface;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IOptionService, OptionService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "WebApi"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();


