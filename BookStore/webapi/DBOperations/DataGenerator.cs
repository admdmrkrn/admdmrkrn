using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace webapi.DBOperetions
{
    public class DataGenerator
    {
        public static void Initailize(IServiceProvider serviceProvider) /*in memory ile alakalı. Burayo Program.cs ile bağlıyruz. Daha sonra program.cs'den burayı çağırarak */ 
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
               if(context.Books.Any())//Hiç veri yoksa demek için Any() Kullandık.
               {
                   return;
               }

               context.Books.AddRange(
                new Book{
                //Id = 1,
                Title= "Lean Startup",
                GenreId=1 ,//Personal Growth
                PageCount = 220,
                PublishDate= new DateTime(2001,02,03)
                },
                new Book{
                   // Id = 2,
                    Title= "At Hırsızı",
                    GenreId= 2 ,//Personal Growth
                    PageCount = 230,
                    PublishDate= new DateTime(2002,08,03)
                },
                new Book{
                    //Id = 3,
                    Title= "At Hırsızı2",
                    GenreId= 2 ,//Personal Growth
                    PageCount = 280,
                    PublishDate= new DateTime(2003,02,03)
                }
                    
               );/*Books'a n tane veri yada Liste eklemek için AddRange kullanıyıruz.*/

               context.SaveChanges();
            }
        }
    }
}