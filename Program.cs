

using Movies.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add the server
builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My First Api",
        Description = "My first project description",
        TermsOfService = new Uri("https://www.facebook.com/mariam.boghdady.5"),
        Contact = new OpenApiContact
        {
            Name = "Mariam Boghdady",
            Email = "mariam.ahmed992@eng-st.cu.edu.eg",
            Url = new Uri("https://www.linkedin.com/in/mariam-boghdady-421080201")
        },
        License = new OpenApiLicense 
        {
            Name = "My License",
            Url = new Uri("https://github.com/MariamBoghdady99/MariamBoghdady99")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
