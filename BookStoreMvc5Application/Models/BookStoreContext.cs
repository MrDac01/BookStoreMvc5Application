using System.Data.Entity;

using BookStoreMvc5Application.Models.Configuration;

namespace BookStoreMvc5Application.Models
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext() : base("name=BookStoreContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuthorConfigaration());
            modelBuilder.Configurations.Add(new CategoryConfigaration());
            modelBuilder.Configurations.Add(new BookConfigaration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}