using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc5Application.ViewModels
{
    public class EditBookViewModel
    {
        [DisplayName("№")]
        [Required]
        [Range(1, 9999999)]
        public int Id { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        [Required(ErrorMessage = "Введите наименование книги")]
        [MinLength(3, ErrorMessage = "Введите кооректное наименование книги (длина более 3 символов)")]
        [DisplayName("Наименование книги")]
        public string Title { get; set; }

        [DisplayName("Автор")]
        [Required(ErrorMessage = "Выберите автора")]
        [Range(1, 9999999)]
        public int? AuthorId { get; set; }

        [DisplayName("Категория")]
        [Required(ErrorMessage = "Выберите категорию")]
        [Range(1, 9999999)]
        public int? CategoryId { get; set; }

        [DisplayName("Количество страниц")]
        [Required(ErrorMessage = "Введите корректное количество страниц")]
        [Range(1, 9999999)]
        public int? Pages { get; set; }

        [DisplayName("Цена")]
        [Required(ErrorMessage = "Введите корректное значение стоимости книги")]
        [Range(1, 9999999)]
        public int? Cost { get; set; }
    }
}