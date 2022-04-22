using BookStoreMvc5Application.Models.Core;

namespace BookStoreMvc5Application.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public int CategoryId { get; set; }

        public int Pages { get; set; }

        public int Cost { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
    }
}