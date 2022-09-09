﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel bookModel);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int BookId);
        public BookModel UpdateBook(int BookId, BookModel bookModel);
        public bool DeleteBook(int BookId);

    }
}