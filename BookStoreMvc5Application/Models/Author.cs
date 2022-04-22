using System.Collections.Generic;

using BookStoreMvc5Application.Models.Core;

namespace BookStoreMvc5Application.Models
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}