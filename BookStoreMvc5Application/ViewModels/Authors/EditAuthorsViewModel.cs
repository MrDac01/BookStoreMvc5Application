using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreMvc5Application.ViewModels.Authors
{
    public class EditAuthorsViewModel
    {
        [DisplayName("№")]
        [Required]
        [Range(1, 9999999)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите фамилию автора")]
        [MinLength(3, ErrorMessage = "Введите кооректную фамилию автора")]
        [DisplayName("Фамилия автора")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите имя автора")]
        [MinLength(3, ErrorMessage = "Введите кооректное имя автора")]
        [DisplayName("Имя автора")]
        public string LastName { get; set; }
    }
}