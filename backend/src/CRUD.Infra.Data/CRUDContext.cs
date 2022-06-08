using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace CRUD.Infra.Data
{
    /// <summary>
    /// Referência de artigo para conseguir criar modelos de configuração
    /// https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core
    /// Rererência de artigo para conseguir criar migration a partir de dominios
    /// https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli
    /// </summary>
    public class CRUDContext : DbContext
    {
        public DbSet<Domain.CRUDAggregate.Cidade> Cidade { get; set; }
        public DbSet<Domain.CRUDAggregate.Pessoa> Pessoa { get; set; }

        public CRUDContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadeEntityTypeConfiguration());
            modelBuilder.Entity<Domain.CRUDAggregate.Cidade>();

            modelBuilder.ApplyConfiguration(new PessoaEntityTypeConfiguration());
            modelBuilder.Entity<Domain.CRUDAggregate.Pessoa>();
        }
    }

    public class CidadeEntityTypeConfiguration : IEntityTypeConfiguration<Domain.CRUDAggregate.Cidade>
    {
        public void Configure(EntityTypeBuilder<Domain.CRUDAggregate.Cidade> orderConfiguration)
        {
            orderConfiguration.ToTable("Cidade", "dbo");

            orderConfiguration.HasKey(o => o.Id);
            orderConfiguration.Property(o => o.Id).UseIdentityColumn();
            orderConfiguration.Property(o => o.Nome).IsRequired();
            orderConfiguration.Property(o => o.UF).IsRequired();
        }
    }

    public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<Domain.CRUDAggregate.Pessoa>
    {
        public void Configure(EntityTypeBuilder<Domain.CRUDAggregate.Pessoa> orderConfiguration)
        {
            orderConfiguration.ToTable("Pessoa", "dbo");

            orderConfiguration.HasKey(o => o.Id);
            orderConfiguration.Property(o => o.Id).UseIdentityColumn();
            orderConfiguration.Property(o => o.Nome).IsRequired();
            orderConfiguration.Property(o => o.CPF).IsRequired();
            orderConfiguration.Property(o => o.Idade).IsRequired();
            orderConfiguration.Property(o => o.Id_Cidade).IsRequired();
            orderConfiguration.HasOne(o => o.Cidade).WithMany().HasForeignKey(o => o.Id_Cidade);
        }
    }
}
