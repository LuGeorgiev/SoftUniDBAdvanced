
namespace BookShop
{
    using BookShop.Data;
    using BookShop.Models;
    using BookShop.Initializer;
    using System.Linq;
    using System;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class StartUp
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

                //P05.Book Titles by Category. Very ugly need to see another approach !!!
                //string categories = Console.ReadLine();
                //string titles = GetBooksByCategory(db, categories);
                //Console.WriteLine(titles);

                //P06.Released Before Date
                var releaseBefore = Console.ReadLine();
                string books = GetBooksReleasedBefore(db, releaseBefore);
                Console.WriteLine(books);

                //P07.	Author Search
                //string endingIn = Console.ReadLine();
                //string authors = GetAuthorNamesEndingIn(db, endingIn);
                //Console.WriteLine(authors);

                //P08.Book Search
                //string containing = Console.ReadLine();
                //string titlesContaining = GetBookTitlesContaining(db, containing);
                //Console.WriteLine(titlesContaining);

                //P09.	Book Search by Author
                //var lastNameStart = Console.ReadLine();
                //string booksByAuthor = GetBooksByAuthor(db, lastNameStart);
                //Console.WriteLine(booksByAuthor);

                //P10.	Count Books
                //int lengthCheck = int.Parse(Console.ReadLine());
                //int count = CountBooks(db, lengthCheck);
                //Console.WriteLine(count);

                //P11.Total Book Copies
                //string copiesByAuthor = CountCopiesByAuthor(db);
                //Console.WriteLine(copiesByAuthor);

                //P12.	Profit by Category
                //string profitByCateory = GetTotalProfitByCategory(db);
                //Console.WriteLine(profitByCateory);

                //P13.Most Recent Books
                //string mostRecentBooks = GetMostRecentBooks(db);
                //Console.WriteLine(mostRecentBooks);

                //P14.Increase Prices
                //IncreasePrices(db);

                //P15.	Remove Books
                //int removedBooks = RemoveBooks(db);
                //Console.WriteLine(removedBooks+" books were deleted");
            }
        }

        public static int RemoveBooks(BookShopContext db)
        {
            //Mine Approach
            //int countRemoved = 0;
            //foreach (var book in db.Books)
            //{
            //    if (book.Copies<4200)
            //    {
            //        db.Books.Remove(book);
            //        countRemoved++;
            //    }
            //}
            //db.SaveChanges();
            //return countRemoved;

            //Bojo approach
            var books = db.Books.Where(b => b.Copies < 4200);
            int result = books.Count();

            db.Books.RemoveRange(books);
            db.SaveChanges();

            return result;
        }

        public static void IncreasePrices(BookShopContext db)
        {
            DateTime releaseBefore = new DateTime(2010, 1, 1);
            
            foreach (var book in db.Books)
            {
                if (book.ReleaseDate< releaseBefore)
                {
                    book.Price += 5m;
                }
            }
            db.SaveChanges();            
        }

        public static string GetMostRecentBooks(BookShopContext db)
        {
            var mostRecentBooksByCategory = db.Categories
                .OrderBy(c=>c.Name)
                .Select(c=>new
                {
                    CatName = c.Name,
                    Books = c.CategoryBooks
                        .OrderByDescending(b=>b.Book.ReleaseDate)
                        .Take(3)
                        .Select(b=> new
                        {
                            Title = b.Book.Title,
                            Year = b.Book.ReleaseDate.Value.Year
                        }).ToList()
                })                
                .ToList();

            var sb = new StringBuilder();
            foreach (var category in mostRecentBooksByCategory)
            {
                sb.AppendLine($"--{category.CatName}");
                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.Year})");
                }

            }

            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var sb = new StringBuilder();

            var categories = db.Categories
                .Select(x => new
                {
                    x.Name,
                    CategoryProfit = x.CategoryBooks
                    .Select(b=>b.Book.Copies*b.Book.Price)
                    .Sum()
                })  
                .OrderByDescending(x=>x.CategoryProfit)
                .ThenBy(x=>x.Name)
                .ToList();               


            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Name} ${category.CategoryProfit:F2}");
            }        

            return sb.ToString().TrimEnd();
        }

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            //Mine approach
            //var sb = new StringBuilder();
            //var copiesByAuthor = db.Authors
            //    .Select(a => new
            //    {
            //        Name = a.FirstName + " " + a.LastName,
            //        Books = a.Books
            //            .Select(b => new
            //            {
            //                b.Copies
            //            }),
            //        Copies = a.Books.Sum(x => x.Copies)
            //    })
            //    .OrderByDescending(z=>z.Copies)
            //    .ToList();

            //foreach (var author in copiesByAuthor)
            //{
            //    sb.AppendLine($"{author.Name} - {author.Copies}");
            //}
            //return sb.ToString().TrimEnd();

            //Bojo approach

            var sb = new StringBuilder();
            var copiesbyAuthor = db.Authors
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName,                    
                    Copies = a.Books
                    .Select(b=>b.Copies)
                    .Sum()
                })
                .OrderByDescending(z => z.Copies)
                .ToList();

            foreach (var author in copiesbyAuthor)
            {
                sb.AppendLine($"{author.Name} - {author.Copies}");
            }
            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext db, int lengthCheck)
        {          
             var booksLongerThan = db.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return booksLongerThan;
        }

        public static string GetBooksByAuthor(BookShopContext db, string lastNameStart)
        {
            //Mine approach
            //            var sb = new StringBuilder();
            //            var booksByAuthor = db.Authors
            //                .Where(a => a.LastName.StartsWith(lastNameStart))
            //                .Select(a => new
            //                {
            //                    Name = a.FirstName + " " + a.LastName,
            //                    Books = a.Books
            //                        .Select(b => new
            //                        {
            //                            b.Title,
            //                            b.BookId
            //                        })
            //                        .OrderBy(b => b.BookId)
            //                })
            //                .ToList();
            //            foreach (var author in booksByAuthor)
            //            {
            //                foreach (var book in author.Books)
            //                {
            //                    sb.AppendLine($"{book.Title} ({author.Name})");
            //;               }
            //            }
            //            return sb.ToString().TrimEnd();

            //Bojo Danchev approach
            string pattern = $"^{lastNameStart.ToLower()}.*$";

            string[] books = db.Books
                .Where(b => Regex.Match(b.Author.LastName.ToLower(), pattern).Success)
                .OrderBy(b=>b.BookId)
                .Select(b=>$"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();

            string result = String.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBookTitlesContaining(BookShopContext db, string containing)
        {
            var sb = new StringBuilder();

            var titles = db.Books
                .Where(x => x.Title.ToLower().Contains(containing.ToLower()))
                .Select(x => new { x.Title })
                .OrderBy(x => x.Title);

            foreach (var title in titles)
            {
                sb.AppendLine(title.Title);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext db, string endingIn)
        {
            var sb = new StringBuilder();

            var authors = db.Authors
                .Where(a => a.FirstName.EndsWith(endingIn,true,CultureInfo.InvariantCulture))
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName
                })
                .OrderBy(a=>a.Name);

            foreach (var author in authors)
            {
                sb.AppendLine(author.Name);
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext db, string releaseBefore)
        {
            //Mine approach
            //DateTime releaseDate = DateTime.ParseExact(releaseBefore, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //var sb = new StringBuilder();
            //var books = db.Books
            //    .Where(b => b.ReleaseDate < releaseDate)
            //    .Select(b => new
            //    {
            //        b.Title,
            //        b.EditionType,
            //        b.Price,
            //        b.ReleaseDate
            //    })
            //    .OrderByDescending(b=>b.ReleaseDate)
            //    .ToList();
            //foreach (var book in books)
            //{
            //    sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            //}
            //return sb.ToString().Trim();

            //Bojidar Danchev Approach

            DateTime releaseDate = DateTime.ParseExact(releaseBefore, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            string[] books = db.Books
                .Where(b => b.ReleaseDate.Value < releaseDate)
                .OrderByDescending(b=>b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}")
                .ToArray();
            string result = String.Join(Environment.NewLine, books);
            return result;
        }

        public static string GetBooksByCategory(BookShopContext db, string categories)
        {
            ////MY WAY
            //var sb = new StringBuilder();
            //var categoryArr = categories.Split();          

            //var books = db.Books
            //    .Include(x => x.BookCategories)
            //    .Select(b=> new
            //    {
            //        Title =b.Title,
            //        Category = b.BookCategories
            //        .Select(y=> new
            //        {
            //            CatName = y.Category.Name
            //        })
            //    })
            //    .OrderBy(x=>x.Title)
            //    .ToList();

            //foreach (var book in books)
            //{
            //    foreach (var catName in book.Category)
            //    {
            //        if (categoryArr.Contains(catName.CatName.ToLower()))
            //        {
            //            sb.AppendLine(book.Title);
            //        }
            //    }

            //}
            //return sb.ToString().TrimEnd();

            //Laboratory excercise by Bojidar Danchev

            string[] categoriesArr = categories.ToLower().Split(new[] { "\t", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string[] titles = db.Books
                .Where(b => b.BookCategories.Any(c => categoriesArr.Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            string result = String.Join(Environment.NewLine, titles);

            return result;
        }

        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
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

        public static string GetBooksByPrice(BookShopContext db)
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

        public static string GetGoldenBooks(BookShopContext db)
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

        public static string GetBooksByAgeRestriction(BookShopContext db, string ageRestriction)
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
                //.Where(e => e.AgeRestriction == (AgeRestriction)restriction) //vice versa
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
