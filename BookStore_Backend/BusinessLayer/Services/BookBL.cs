using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return this.bookRL.AddBook(bookModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BookModel GetBookById(int BookId)
        {
            try
            {
                return this.bookRL.GetBookById(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BookModel UpdateBook(int BookId, BookModel bookModel)
        {
            try
            {
                return this.bookRL.UpdateBook(BookId, bookModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteBook(int BookId)
        {
            try
            {
                return this.bookRL.DeleteBook(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
