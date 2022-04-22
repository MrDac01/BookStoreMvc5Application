using System;
using System.ComponentModel;

namespace BookStoreMvc5Application.ViewModels
{
    public class IndexViewModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Дата добавления")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Наименование книги")]
        public string Title { get; set; }

        [DisplayName("Автор")]
        public string AuthorName { get; set; }

        [DisplayName("Категория")]
        public string CategoryName { get; set; }

        [DisplayName("Страниц в книге")]
        public int Pages { get; set; }

        [DisplayName("Цена")]
        public int Cost { get; set; }
    }
}