using BookStoreMvc5Application.Models.Core;

namespace BookStoreMvc5Application.Models.Configuration
{
    class CategoryConfigaration : GenericEntityTypeConfiguration<Category>
    {
        public CategoryConfigaration()
        {
            ToTable("Category");

            ApplyConfigurationForEntity();

            Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(250)
                .IsRequired();
        }


    }
}