using System;
using System.Linq;
using webapi.DBOperetions;

namespace webapi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {

        public readonly BookStoreDbContext _dbContext;

        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book is null)
             throw new InvalidOperationException("Kitap bulunamadı!");
                     
             BookDetailViewModel vm = new BookDetailViewModel();
             vm.Title=book.Title;
             vm.PageCount=book.PageCount;
             vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyy");
             return vm;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre      { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}