using AutoMapper;
using Library.API.DTOs.Book;
using Library.Models.Entities;

namespace Library.API.Mappers;

public class BookMapper : Profile
{
    public BookMapper()
    {
        CreateMap<Book, BookDTO>().ReverseMap();
    }
}
