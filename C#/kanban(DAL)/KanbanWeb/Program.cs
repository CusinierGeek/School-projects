using System.Globalization;
using KanbanDonnees.DAO.EnMemoire;
using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.DAO.Mysql;
using KanbanDonnees.DAO.Sqlite;
// using KanbanDonnees.DAO.Mysql;
// using KanbanDonnees.DAO.Sqlite;
using KanbanWeb;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

const string politiqueCors = "PolitiqueCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: politiqueCors,
        policy =>
        {
            policy.WithOrigins("*")
                .WithMethods("POST", "PUT", "DELETE", "GET", "PATCH")
                .AllowAnyHeader();
        });
});


///////////////////////////////////////CE QUI VOUS CONCERNE COMMENCE ICI ///////////////////////////////////////////////

// On récupère les configurations dans le fichier appsettings.Development.json (même principe que App.config en XML)
bool creerBd = Convert.ToBoolean(builder.Configuration.GetSection("Persistance:CreerBd").Value);
string typePersistance = builder.Configuration.GetSection("Persistance:TypePersistance").Value ?? "memoire";
string chaineDeConnexion = builder.Configuration.GetSection("Persistance:ChaineDeConnexion").Value ?? "";

IGestionPersistance? gestionPersistance = null;

// On détermine les DAO à utiliser par l'application
// Utilisation de MySql
if (typePersistance == "mysql")
{
    builder.Services.AddScoped<IUtilisateurDao>(x => new UtilisateurMysqlDao(chaineDeConnexion));
    builder.Services.AddScoped<ICarteDao>(x =>
        new CarteMysqlDao(chaineDeConnexion, (UtilisateurMysqlDao)x.GetRequiredService<IUtilisateurDao>()));
    builder.Services.AddScoped<IListeDao>(x =>
        new ListeMysqlDao(chaineDeConnexion, (CarteMysqlDao)x.GetRequiredService<ICarteDao>()));
    builder.Services.AddScoped<ITableauDao>(x =>
        new TableauMysqlDao(chaineDeConnexion, (ListeMysqlDao)x.GetRequiredService<IListeDao>()));
    gestionPersistance = new GestionPersistanceMysql(chaineDeConnexion);
}
// Utilisation de Sqlite
else if (typePersistance == "sqlite")
{
    builder.Services.AddScoped<IUtilisateurDao>(x => new UtilisateurSqliteDao(chaineDeConnexion));
    builder.Services.AddScoped<ICarteDao>(x =>
        new CarteSqliteDao(chaineDeConnexion, (UtilisateurSqliteDao)x.GetRequiredService<IUtilisateurDao>()));
    builder.Services.AddScoped<IListeDao>(x =>
        new ListeSqliteDao(chaineDeConnexion, (CarteSqliteDao)x.GetRequiredService<ICarteDao>()));
    builder.Services.AddScoped<ITableauDao>(x =>
        new TableauSqliteDao(chaineDeConnexion, (ListeSqliteDao)x.GetRequiredService<IListeDao>()));
    gestionPersistance = new GestionPersistanceSqlite(chaineDeConnexion);
}
// Utilisation de la persistance en mémoire
else if (typePersistance == "memoire")
{
    builder.Services.AddScoped<IUtilisateurDao, UtilisateurEnMemoireDao>();
    builder.Services.AddScoped<ICarteDao, CarteEnMemoireDao>();
    builder.Services.AddScoped<IListeDao, ListeEnMemoireDao>();
    builder.Services.AddScoped<ITableauDao, TableauEnMemoireDao>();
}
else
{
    throw new ApplicationException("Impossible d'initialiser la persistance de l'application.");
}

Console.WriteLine($"Type de la persistance : {typePersistance}");

// Création et alimentation (seeding) de la BD
if (creerBd && gestionPersistance != null)
{
    Console.WriteLine("Alimentation de la solution de persistance...");
    gestionPersistance.CreerPersistanceEtInsererDonnees();
}
//////////////////////////////////////////////// ET CE TERMINE ICI /////////////////////////////////////////////////////

builder.Services.AddAutoMapper(typeof(ConfigurationMapping));
builder.Services.AddControllers().AddNewtonsoftJson();
// Configuration Swagger/OpenAPI : https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ILogger<Program>? logger = app.Services.GetService<ILogger<Program>>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();
//app.UseHttpsRedirection();
app.UseCors(politiqueCors);
app.MapControllers();

// Pour faire fonctionner l'app VueJs correctement
app.MapWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api"), appBuilder =>
{
    appBuilder.UseRouting();
    appBuilder.UseEndpoints(ep => { ep.MapFallbackToFile("index.html"); });
});

app.Run();