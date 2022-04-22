using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BookStoreMvc5Application.Models.Core
{
    abstract class GenericEntityTypeConfiguration<TModel> : EntityTypeConfiguration<TModel> where TModel : Entity
    {
        protected void ApplyConfigurationForEntity()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CreatedAt)
               .HasColumnName("CreatedAt")
               .IsRequired();

            Property(x => x.UpdatedAt)
               .HasColumnName("UpdateAt")
               .IsOptional();
        }
    }
}