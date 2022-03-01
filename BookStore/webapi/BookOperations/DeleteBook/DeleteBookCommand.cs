using System;
using System.Linq;
using webapi.DBOperetions;

namespace webapi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public readonly BookStoreDbContext _dbContext;

        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
                if (book is null)
                    {
                        throw new InvalidOperationException("Silinecek ");

                    }

            _dbContext.Remove(book);
            _dbContext.SaveChanges();
         
        }  
    }
}
