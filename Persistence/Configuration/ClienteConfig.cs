using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    //Mapea todas las caracteristicas de nuestra tabla en base de datos
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //mapea las tablas 
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(p => p.Nombre)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.Apellido)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.FechaNacimiento)
                .IsRequired();

            builder.Property(p=>p.Telefono)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.Direccion)
            .HasMaxLength(120)
            .IsRequired();

            builder.Property(p => p.Edad);//automaticamente lo toma como entero

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);


            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
