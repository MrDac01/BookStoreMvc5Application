using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc5Application.ViewModels
{
    public class CreateBookViewModel
    {
        [Required(ErrorMessage = "Введите наименование книги")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Введите кооректное наименование книги (длина: не менее 3 симовлов и не более 250 символов)")]
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