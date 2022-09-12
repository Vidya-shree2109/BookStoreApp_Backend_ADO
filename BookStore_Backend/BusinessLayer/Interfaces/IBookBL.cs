using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookPostModel AddBook(BookPostModel bookPostModel);
        public List<BookResponseModel> GetAllBooks();
        public BookResponseModel GetBookById(int BookId);

        public BookResponseModel UpdateBooks(int BookId, BookPostModel bookPostModel);
        public bool DeleteBook(int BookId);

    }
}
