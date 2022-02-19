using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webapi.DBOperetions;

namespace webapi.AddControllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase
    {
     

         private readonly BookStoreDbContext _context; // Uygulama .çerisinden değiştirilemesin diye readOnly yaptık.

         public BookController(BookStoreDbContext context)
         {
             _context=context;
         }

        //    
        //     new Book{
        //         Id = 1,
        //         Title= "Lean Startup",
        //         GenreId=1 ,//Personal Growth
        //         PageCount = 220,
        //         PublishDate= new DateTime(2001,02,03)
        //     },
        //     new Book{
        //         Id = 2,
        //         Title= "At Hırsızı",
        //         GenreId= 2 ,//Personal Growth
        //         PageCount = 230,
        //         PublishDate= new DateTime(2002,08,03)
        //     },
        //     new Book{
        //         Id = 3,
        //         Title= "At Hırsızı2",
        //         GenreId= 2 ,//Personal Growth
        //         PageCount = 280,
        //         PublishDate= new DateTime(2003,02,03)
        //     }
        //    
        //};

        // GET

        [HttpGet]
        public IActionResult GetBooks()
        {
           GetBooksQuery query = new GetBooksQuery(_context);
           var result = query.Handle();
           return Ok(result);

        }
          
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book = _context.Books.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }
         
        [HttpPost]

        public IActionResult AddBook([FromBody] Book newBook )
        {
           var book =  _context.Books.SingleOrDefault(x=>x.Title==newBook.Title);
           if(book is not null)
           return BadRequest();
           _context.Books.Add(newBook);
           _context.SaveChanges();  //database ten çektiğimiz için artık SaveChanges() ile her değiştirdiğimizde kayıt yapmamız
            return Ok();   

        }
        // PUT Yöntemi
        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book =  _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
           
            _context.SaveChanges();

            return Ok();

        }
          //DELETE
        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book =  _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
             
            _context.SaveChanges(); 

            return Ok();
        }





    }
}
