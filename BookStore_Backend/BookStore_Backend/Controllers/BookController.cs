using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStore_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = bookBL.AddBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully.. !!" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to add book.. !" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                List<BookModel> listOfbooks = new List<BookModel>();
                listOfbooks = this.bookBL.GetAllBooks();
                return Ok(new { success = true, Message = "Data Fetched Successfully.. !!", data = listOfbooks });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetBookById")]
        public IActionResult GetBookById(int BookId)
        {
            try
            {
                var result = this.bookBL.GetBookById(BookId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, message = $"Failed to get book id = {BookId}" });
                }
                return this.Ok(new { success = true, message = $"Book id = {BookId} fetched Successfully", data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost("UpdateBook")]
        public IActionResult UpdateBook(int BookId, BookModel bookModel)
        {
            try
            {
                var result = this.bookBL.UpdateBook(BookId, bookModel);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "Failed to update book.. !" });
                }

                return this.Ok(new { success = true, Message = "Book Updated Sucessfully.. !!", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(BookId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Failed to delete book - BookId : {BookId}" });
                }

                return this.Ok(new { success = true, Message = $"Book Deleted Sucessfully - BookId : {BookId}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
