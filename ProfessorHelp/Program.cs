using ProfessorHelp.Data;
using ProfessorHelp.Services.Automapper;
using ProfessorHelp.Services.Bootstrapper;
using ProfessorHelp.Shared.Exception.Filter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => option.LowercaseUrls = true);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAplication(builder.Configuration);
builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfiles());
}).CreateMapper());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
