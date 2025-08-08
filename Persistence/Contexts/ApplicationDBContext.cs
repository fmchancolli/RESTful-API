using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class ApplicationDBContext: DbContext
    {

        public readonly IDateTimeService _dateTime;
        //cualquier cambio detextado se mantendra en la db
        //arregla las propiedades de nav en las entidades
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IDateTimeService dateTime) :base (options) 
        {
            ChangeTracker.QueryTrackingBehavior=QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        public DbSet<Cliente> Clientes { get; set; }

        //sobre escribimos metodo SaveChangesAsync
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //actualiza cambio de auditoria al hacer un cambio
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;

                }

            }
            return base.SaveChangesAsync(cancellationToken);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
