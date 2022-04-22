using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookStoreMvc5Application.ViewModels.Authors
{
    public class IndexAuthorsViewModel
    {
        [DisplayName("№")]
        public int Id { get; set; }
        
        [DisplayName("Дата добавления")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        
        [DisplayName("Имя")]
        public string FirstName { get; set; }
    }
}