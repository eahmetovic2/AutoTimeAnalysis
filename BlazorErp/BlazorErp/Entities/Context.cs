using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Base;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Entities.Models.Sifarnik;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorErp.Entities
{
    //public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityKorisnik>
    //{
    //    public ApplicationDbContext(
    //        DbContextOptions options,
    //        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    //    {
    //    }
    //}

    public class Context : KeyApiAuthorizationDbContext<IdentityKorisnik, Uloga, int, IdentityUserClaim<int>, KorisnikUloga, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    //public class Context : ApiAuthorizationDbContext<IdentityKorisnik>
    {
        #region Korisnik
        public DbSet<Token> Tokeni { get; set; }
        public DbSet<Modul> Moduli { get; set; }
        public DbSet<PravoGrupa> PravoGrupe { get; set; }
        public DbSet<PravoObjekat> PravoObjekti { get; set; }
        public DbSet<PravoAkcija> PravoAkcije { get; set; }
        public DbSet<PravoAkcijaUloga> PravoAkcijaUloge { get; set; }
        public DbSet<KorisnikTipDodatneInformacije> KorisnikTipoviDodatneInformacije { get; set; }
        public DbSet<KorisnikUlogaDodatnaInformacija> KorisnikUlogaDodatneInformacije { get; set; }
        public DbSet<UlogaTipDodatneInformacije> UlogaTipoviDodatneInformacije { get; set; }
        public DbSet<PravoUpravljanjaKorisnikom> PravaUpravljanjaKorisnicima { get; set; }
        #endregion


        #region Administracija
        public DbSet<Postavke> Postavke { get; set; }
        #endregion


        #region Sifarnik
        #endregion

        #region Log
        public DbSet<VrstaLogAkcija> VrsteLogAkcija { get; set; }
        public DbSet<LogKategorija> LogKategorije { get; set; }
        public DbSet<LogLevel> LogLeveli { get; set; }
        public DbSet<LogAkcija> LogAkcije { get; set; }
        public DbSet<LogEntitet> LogEntiteti { get; set; }
        #endregion

        public DbSet<Dokument> Dokumenti { get; set; }

        public Context(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public override int SaveChanges()
        {
            return SaveChanges(null);
        }

        public int SaveChanges(string korisnickoIme)
        {
            var items = new Dictionary<object, object>();

            //Pravimo listu entry koje moramo poslati u log. Te čuvamo njihove state, jer poslije spašavanja biće urađen reset.
            var entries = ChangeTracker.Entries().Where(e => (e.State == EntityState.Added)
            || (e.State == EntityState.Modified)
            || (e.State == EntityState.Deleted))
            .Select(x => new EntityEntryLog
            {
                Entry = x,
                State = x.State
            }
            ).ToList();

            foreach (var entry in ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                if (entry.Entity is BazniModel)
                {
                    var entityBase = entry.Entity as BazniModel;

                    if (entry.State == EntityState.Added)
                    {
                        entityBase.DatumKreiranja = DateTime.Now;
                        entityBase.CreatedBy = korisnickoIme;
                    }

                    entityBase.DatumIzmjene = DateTime.Now;
                    entityBase.ModifiedBy = korisnickoIme;
                }

                if (entry.Entity is EntityDated)
                {
                    var entityBase = entry.Entity as EntityDated;

                    if (entry.State == EntityState.Added)
                    {
                        entityBase.DatumKreiranja = DateTime.Now;
                    }

                    entityBase.DatumIzmjene = DateTime.Now;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (entry.Entity is BazniModel)
                {

                    var entityBase = entry.Entity as BazniModel;
                    base.Entry(entityBase).Property("IsDeleted").CurrentValue = true;
                    entityBase.DatumIzmjene = DateTime.Now;
                    entityBase.ModifiedBy = korisnickoIme;
                    entry.State = EntityState.Modified;
                }
            }

            var result = base.SaveChanges();

            //State se mijenja nakon spašavanja, tako da ovdje vraćamo orginalne vrijednosti
            //var hub = new MessageHub();
            //entries.ForEach(x => hub.Publish<EntityEntryLog>(x));

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceFlowCodes>()
               .HasKey(x => x.UserCode);

            modelBuilder.Entity<PersistedGrant>()
               .HasKey(x => x.Key);

            modelBuilder.Entity<PravoAkcija>()
               .HasIndex(a => a.Sifra)
               .IsUnique();

            modelBuilder.Entity<PravoGrupa>()
                .HasIndex(a => a.Sifra)
                .IsUnique();

            modelBuilder.Entity<Modul>()
                .HasIndex(a => a.Sifra)
                .IsUnique();

            modelBuilder.Entity<PravoObjekat>()
                .HasIndex(a => a.Sifra)
                .IsUnique();

            modelBuilder.Entity<PravoAkcijaUloga>()
                        .HasKey(c => new { c.PravoAkcijaId, c.UlogaId });


            #region Identity

            modelBuilder.Entity<KorisnikUloga>()
                .HasOne(e => e.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<KorisnikUloga>()
                .HasOne(e => e.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KorisnikUlogaDodatnaInformacija>()
                        .HasOne(p => p.KorisnikUloga)
                        .WithMany(b => b.KorisnikUlogaDodatnaInformacija)
                        .HasForeignKey(c => new { c.KorisnikId, c.UlogaId });

            modelBuilder.Entity<UlogaTipDodatneInformacije>()
                        .HasKey(c => new { c.UlogaId, c.KorisnikTipDodatneInformacijeId });

            modelBuilder.Entity<PravoUpravljanjaKorisnikom>()
                        .HasKey(c => new { c.UlogaUpraviteljaId, c.UlogaUpravljanogId });

            modelBuilder.Entity<PravoUpravljanjaKorisnikom>()
                .HasOne(e => e.UlogaUpravitelja)
                .WithMany(x => x.PravaUpravljanja)
                .HasForeignKey(e => e.UlogaUpraviteljaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PravoUpravljanjaKorisnikom>()
                .HasOne(e => e.UlogaUpravljanog)
                .WithMany(x => x.PravaUpravljanosti)
                .HasForeignKey(e => e.UlogaUpravljanogId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UlogaTipDodatneInformacije>()
                .HasOne(e => e.Uloga)
                .WithMany(x => x.TipoviDodatneInformacije)
                .HasForeignKey(e => e.UlogaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PravoAkcijaUloga>()
                .HasOne(e => e.Uloga)
                .WithMany(x => x.PravoAkcijaUloge)
                .HasForeignKey(e => e.UlogaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            // Omogucavanje soft delete na nivou cijelog sistema
            // U svaku podklasu BaznogModela se doda novo polje IsDeleted koje se koristi za brisanje
            modelBuilder.Model.GetEntityTypes()
                       .Where(entityType => entityType.ClrType.IsSubclassOf(typeof(BazniModel))
                                            && !entityType.ClrType.IsSubclassOf(typeof(IdentityKorisnik)))
                       .ToList()
                       .ForEach(entityType =>
                       {
                           modelBuilder.Entity(entityType.ClrType).Property<Boolean>("IsDeleted");
                           var parameter = Expression.Parameter(entityType.ClrType, "e");
                           var body = Expression.Equal(
                               Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
                           Expression.Constant(false));
                           modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
                       });

        }
    }
}
