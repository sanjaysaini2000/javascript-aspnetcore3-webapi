using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Models
{
    public static class InitializeDB
    {
        public static void InitDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                CreateDb(serviceScope.ServiceProvider.GetService<BookStoreDbContext>());

            }

        }

        static void CreateDb(BookStoreDbContext context)
        {
            System.Console.WriteLine("Apply initial migration...");
            context.Database.Migrate();
            if (!context.BookItems.Any())
            {
                System.Console.WriteLine("Adding initial data...");
                context.BookItems.AddRange(
                   new BookItem { Title = "History of C#", Author = "Amit", Publisher = "Westland", Genre = "History", Price = 12 },
                   new BookItem { Title = "Future of C#", Author = "Sanjay", Publisher = "Westland", Genre = "Fiction", Price = 14 }
                );
                context.SaveChanges();

            }
            else
            {
                System.Console.WriteLine("DB already populated...");

            }

        }

    }
}