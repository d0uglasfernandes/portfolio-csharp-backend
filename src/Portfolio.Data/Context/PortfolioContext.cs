using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace Portfolio.Data.Context
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This method should be empty to apply migrations correctly (commented to skip sonarqube code smells)
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configurando Mapeamentos de bancos de dados.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Configurando para armazenar todos dados em UpCase.
            var converter = new ValueConverter<string, string>(
                v => v.ToUpper(), //Input
                v => v.ToUpper() //Output
            );

            //Configurando Usuario para não alterar os dados gravados (Case Sensitive).
            /*foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.Name == "Portfolio.Domain.Entities.Comum.UsuarioEntity")
                {
                    continue;
                }
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(converter);
                    }
                }
            }*/
        }
    }
}