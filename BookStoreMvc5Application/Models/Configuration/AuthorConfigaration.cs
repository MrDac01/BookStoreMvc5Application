using BookStoreMvc5Application.Models.Core;

namespace BookStoreMvc5Application.Models.Configuration
{
    class AuthorConfigaration : GenericEntityTypeConfiguration<Author>
    {
        public AuthorConfigaration()
        {
            ToTable("Author");

            ApplyConfigurationForEntity();

            Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(30)
                .IsRequired();

            Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}