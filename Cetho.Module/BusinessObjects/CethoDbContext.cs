using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;

namespace Cetho.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class CethoContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
        var optionsBuilder = new DbContextOptionsBuilder<CethoEFCoreDbContext>();
            optionsBuilder.UseNpgsql(";")            
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new CethoEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class CethoDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CethoEFCoreDbContext> {
	public CethoEFCoreDbContext CreateDbContext(string[] args) {
		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
        var optionsBuilder = new DbContextOptionsBuilder<CethoEFCoreDbContext>();
        optionsBuilder.UseSqlServer("Host=localhost;Database=cetho;Username=postgres;Password=1");
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        return new CethoEFCoreDbContext(optionsBuilder.Options);
    }
}
[TypesInfoInitializer(typeof(CethoContextInitializer))]
public class CethoEFCoreDbContext : DbContext {
	public CethoEFCoreDbContext(DbContextOptions<CethoEFCoreDbContext> options) : base(options) {
	}
	//public DbSet<ModuleInfo> ModulesInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
    }
}
