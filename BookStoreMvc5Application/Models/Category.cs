using System.Collections.Generic;

using BookStoreMvc5Application.Models.Core;

namespace BookStoreMvc5Application.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}