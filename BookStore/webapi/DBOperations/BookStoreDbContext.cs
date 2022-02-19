using System;
using Microsoft.EntityFrameworkCore;

namespace webapi.DBOperetions
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext>options): base(options)
        {

        }
         public DbSet<Book>Books{get;set;}
  
    }
}