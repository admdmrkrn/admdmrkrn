using System;
using System.Linq;
using webapi.DBOperetions;

namespace webapi.BookOperations.CreateBook
{

    public class CreateBookCommand
    {
        public CreateBookModel Model {get; set;}

        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book =  _dbContext.Books.SingleOrDefault(x=>x.Title==Model.Title);
          
           if(book is not null)
          // return BadRequest();

          throw new InvalidOperationException("Kitap zaten Mevcut :)"); 
          // return Badrequest(); yerine bu işlemi yapıyoruz işlem hatalı ise bu hata
          // -mesajını ver diyoruz.
            
          book=new Book();
          book.Title=Model.Title;
          book.PublishDate=Model.PublishDate;
          book.PageCount=Model.PegeCount;
          book.GenreId=Model.GenreId;

           _dbContext.Books.Add(book);
           _dbContext.SaveChanges(); 
            //database ten çektiğimiz için artık SaveChanges() ile her değiştirdiğimizde kayıt yapmamız

        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PegeCount  { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}     