# CethoENTFW Connection PostgreSQL

1. Install the Npgsql.EntityFrameworkCore.PostgreSQL﻿ package. Note its Microsoft.EntityFrameworkCore package reference. That Entity Framework’s version must match the version that your solution uses (currently 6.0.x).
2. Change your application’s connection string.
    Cetho\Cetho.Blazor.Server\appsettings.json :
    "ConnectionStrings": {
        "ConnectionString": "Host=localhost;Database=my_db;Username=postgres;Password=qwerty"
    }
3. Cetho\Cetho.Module\BusinessObjects\CethoDbContext.cs:
    public class Postgre1DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Postgre1EFCoreDbContext> {
    public Postgre1EFCoreDbContext CreateDbContext(string[] args) {
        //throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
        var optionsBuilder = new DbContextOptionsBuilder<Postgre1EFCoreDbContext>();
        //optionsBuilder.UseSqlServer("Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MyDBName");
        optionsBuilder.UseNpgsql("Host=localhost;Database=my_db2;Username=postgres;Password=qwerty");
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        return new Postgre1EFCoreDbContext(optionsBuilder.Options);
    }
4. Cetho\Cetho.Module\BusinessObjects\CethoDbContext.cs:
   public class Postgre1ContextInitializer : DbContextTypesInfoInitializerBase {
    protected override DbContext CreateDbContext() {
        var optionsBuilder = new DbContextOptionsBuilder<Postgre1EFCoreDbContext>()
            //.UseSqlServer(";")
            .UseNpgsql(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new Postgre1EFCoreDbContext(optionsBuilder.Options);
    }
}
5. Cetho\Cetho.Blazor.Server\Startup.cs :
    public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        //...
        builder.ObjectSpaceProviders
            .AddSecuredEFCore().WithDbContext<Postgre1.Module.BusinessObjects.Postgre1EFCoreDbContext>((serviceProvider, options) => {
                // ...
                // options.UseSqlServer(connectionString);
                options.UseNpgsql(connectionString);
                options.UseChangeTrackingProxies();
                options.UseObjectSpaceLinkProxies();
            })
