
namespace BookShop
{
    using BookShop.Data;
    using BookShop.Models;
    using BookShop.Initializer;
    using System.Linq;
    using System;
    using System.Text;
    using Microsoft.EntityFrameworkCore;

    class StartUp
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                //P00
                //DbInitializer.ResetDatabase(db);

                //P01.	Age Restriction
                //string ageRestriction = Console.ReadLine();
                //string books = GetBooksByAgeRestriction(db, ageRestriction);
                //Console.WriteLine(books);

                //P02.	Golden Books
                //string goldenBook = GetGoldenBooks(db);
                //Console.WriteLine(goldenBook);

                //P03.	Books by Price
                //string booksByPrice = GetBooksByPrice(db);
                //Console.WriteLine(booksByPrice);

                //P04.	Not Released In
                //int year = int.Parse(Console.ReadLine());
                //string booksNotRealeasedIn = GetBooksNotRealeasedIn(db, year);
                //Console.WriteLine(booksNotRealeasedIn);

                //P05.Book Titles by Category
                string categories = Console.ReadLine();
                string titles = GetBooksByCategory(db, categories);
                Console.WriteLine(titles);
            }
        }

        private static string GetBooksByCategory(BookShopContext db, string categories)
        {
            var sb = new StringBuilder();
            var categoryArr = categories.Split();          

            var books = db.Books
                .Include(x => x.BookCategories)
                .Select(b=> new
                {
                    Title =b.Title,
                    Category = b.BookCategories
                    .Select(y=> new
                    {
                        CatName = y.Category.Name
                    })
                })
                .OrderBy(x=>x.Title)
                .ToList();

            foreach (var book in books)
            {
                foreach (var catName in book.Category)
                {
                    if (categoryArr.Contains(catName.CatName.ToLower()))
                    {
                        sb.AppendLine(book.Title);
                    }
                }

            }

            return sb.ToString().TrimEnd();
        }

        private static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            var sb = new StringBuilder();

            var books = db.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title
                });
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }

        private static string GetBooksByPrice(BookShopContext db)
        {
            var sb = new StringBuilder();

            var booksByPrice = db.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                });

            foreach (var book in booksByPrice)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        private static string GetGoldenBooks(BookShopContext db)
        {
            var sb = new StringBuilder();

            var goldenBooks = db.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    bookTitle = b.Title
                });

            foreach (var book in goldenBooks)
            {
                sb.AppendLine(book.bookTitle);
            }

            return sb.ToString().TrimEnd();
        }

        private static string GetBooksByAgeRestriction(BookShopContext db, string ageRestriction)
        {
            int restriction = 0;
            if (ageRestriction.ToLower() == "teen")
            {
                restriction = 1;
            }
            else if (ageRestriction.ToLower() == "adult")
            {
                restriction = 2;
            }

            var books = db.Books
                .Where(e => (int)e.AgeRestriction == restriction)
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b=>b.Title);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().Trim();            
        }
        
    }
}
