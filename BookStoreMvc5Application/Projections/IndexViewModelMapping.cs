
using AutoMapper;

using BookStoreMvc5Application.Models;
using BookStoreMvc5Application.ViewModels;

namespace BookStoreMvc5Application.Projections
{
    public class IndexViewModelMapping : Profile
    {
        public IndexViewModelMapping()
        {
            CreateMap<Book, IndexViewModel>()
                .ForMember(x => x.AuthorName, y =>
                  {
                      y.MapAtRuntime();
                      y.MapFrom(z => z.Author.LastName + " " + z.Author.FirstName);
                  })

                .ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.Name))
                ;
        }
    }
}