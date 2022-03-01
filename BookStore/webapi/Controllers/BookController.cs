using System;
using Microsoft.AspNetCore.Mvc;
using webapi.BookOperations.CreateBook;
using webapi.BookOperations.DeleteBook;
using webapi.BookOperations.GetBookDetail;
using webapi.BookOperations.GetBooks;
using webapi.BookOperations.UpdateBook;
using static webapi.BookOperations.CreateBook.CreateBookCommand;
using static webapi.BookOperations.GetBookDetail.GetBookDetailQuery;
using static webapi.BookOperations.UpdateBook.UpdateBookCommand;
//using static webapi.BookOperations.GetBooks.GetBooksQuery;
//using static webapi.BookOperations.DeleteBook.DeleteBookCommand;
using webapi.DBOperetions;

namespace webapi.AddControllers
{
    [ApiController]
     [Route("[Controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context; 
        // Uygulama .çerisinden değiştirilemesin diye readOnly yaptık.

        public BookController(BookStoreDbContext context)
         {
             _context=context;
         }

        // GET ////////////////////////////////////////////////////////
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }



        //[HttpGet]
        //public IActionResult GetBooks()
        //{
        //    GetBooksQuery query = new GetBooksQuery(_context);
        //    var result = query.Handle();
        //    return Ok(result);
        //}

        //Get(id)////////////////////////////////
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                 query.BookId = id;
                 result = query.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);      
            }
           return Ok(result);
          
        }
        //// ///////////// post //////////////////////////////
        
        [HttpPost]
       public IActionResult AddBook([FromBody] CreateBookModel newBook )
      // public IActionResult AddBook([FromBody] CreateBookModel newBook)
         {
             CreateBookCommand command = new CreateBookCommand(_context);
             try
             {
                  command.Model= newBook;    
                  command.Handle();
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
             return Ok(); 
            
           /////////////////////////////////////////////////////////////////

        }
        // PUT Yöntemi
        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model = updatedBook;
                command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
       
        }

          //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return Ok();
        }





    }
}
