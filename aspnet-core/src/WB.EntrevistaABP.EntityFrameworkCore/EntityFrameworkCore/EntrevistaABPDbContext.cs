using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using WB.EntrevistaABP.Pasajeros;
using WB.EntrevistaABP.Reservas;
using WB.EntrevistaABP.Viajes;

namespace WB.EntrevistaABP.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class EntrevistaABPDbContext :
    AbpDbContext<EntrevistaABPDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Viaje> Viajes { get; set; } 
    public DbSet<Pasajero> Pasajeros { get; set; }   

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public EntrevistaABPDbContext(DbContextOptions<EntrevistaABPDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EntrevistaABPConsts.DbTablePrefix + "YourEntities", EntrevistaABPConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        // builder.Entity<ApplicationUser>()
        // .HasDiscriminator<string>("UserType")
        // .HasValue<ApplicationUser>("ApplicationUser")
        // .HasValue<Pasajero>("Pasajero");

            builder.Entity<Pasajero>(b => 
            {
                b.ToTable(EntrevistaABPConsts.DbTablePrefix + "Pasajeros", EntrevistaABPConsts.DbSchema);
                b.ConfigureByConvention(); 
                b.Property(x => x.Nombre).IsRequired();
                b.Property(x => x.Apellido).IsRequired();
                b.Property(x => x.DNI).IsRequired().HasMaxLength(10);
                b.Property(x => x.Fecha_de_nacimiento).IsRequired();

                b.HasOne<IdentityUser>() 
                .WithOne()
                .HasForeignKey<Pasajero>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasMany( x => x.Reservas)
                .WithOne(x => x.Pasajero)
                .HasForeignKey(x => x.PasajeroId)
                .OnDelete(DeleteBehavior.Cascade);
                
                
            });

            builder.Entity<Viaje>(b =>
            {
                b.ToTable(EntrevistaABPConsts.DbTablePrefix + "Viajes",
                EntrevistaABPConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Origen).IsRequired();
                b.Property(x => x.Destino).IsRequired();
                b.Property(x => x.Fecha_de_llegada).IsRequired();
                b.Property(x => x.Fecha_de_llegada).IsRequired();
                b.Property(x => x.Medio_transporte).IsRequired();

                
                b.HasMany( x => x.Reservas)
                .WithOne(x => x.Viaje)
                .HasForeignKey(x => x.ViajeId)
                .OnDelete(DeleteBehavior.Cascade);
            
            });
            
            builder.Entity<Reserva>(b =>
            {
                b.ToTable(EntrevistaABPConsts.DbTablePrefix + "Reservas",
                EntrevistaABPConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Coordinador);
                // b.HasOne(r => r.pasajero)
                // .WithMany(p => p.Reservas)
                // .HasForeignKey(r => r.PasajeroId)
                // .OnDelete(DeleteBehavior.Restrict);

                //  b.HasOne(r => r.viaje) 
                // .WithMany(p => p.Reservas)
                // .HasForeignKey(r => r.ViajeId)
                // .OnDelete(DeleteBehavior.Restrict);
            });

        
    }
}
