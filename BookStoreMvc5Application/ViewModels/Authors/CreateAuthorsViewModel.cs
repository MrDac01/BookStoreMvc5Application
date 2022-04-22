using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreMvc5Application.ViewModels.Authors
{
    public class CreateAuthorsViewModel
    {
        [Required(ErrorMessage = "Введите фамилию автора")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Введите кооректную фамилию автора (длина: не менее 3 символа")]
        [DisplayName("Фамилия автора")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите автора автора")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Введите кооректное имя автора (длина: не менее 2 символа")]
        [DisplayName("Имя автора")]
        public string LastName { get; set; }
    }
}