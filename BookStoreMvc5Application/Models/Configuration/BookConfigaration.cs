using BookStoreMvc5Application.Models.Core;

namespace BookStoreMvc5Application.Models.Configuration
{
    class BookConfigaration : GenericEntityTypeConfiguration<Book>
    {
        public BookConfigaration()
        {
            ToTable("Book");

            ApplyConfigurationForEntity();

            Property(x => x.Title)
                .HasColumnName("title")
                .HasMaxLength(250)
                .IsRequired();

            Property(x => x.AuthorId)
                .HasColumnName("AuthorId")
                .IsRequired();

            Property(x => x.CategoryId)
                .HasColumnName("CategoryId")
                .IsRequired();

            Property(x => x.Pages)
                .HasColumnName("Pages")
                .IsRequired();

            Property(x => x.Cost)
                .HasColumnName("Cost")
                .IsRequired();

            HasRequired(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Category)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
