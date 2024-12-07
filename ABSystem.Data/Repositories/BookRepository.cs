using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Data.Objects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly ABSystemDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(ABSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
         * This method is for adding a booking to the database
         */
        public int AddBook(Book book)
        {
            this._context.Books.Add(book);

            this._context.SaveChanges();

            return book.Id;
        }

        public Book GetBookById(int bookId)
        {
            return this._context.Books
                .Include(b => b.Room)
                .FirstOrDefault(b => b.Id == bookId)!;
        }

        public IEnumerable<Book> GetBooks()
        {


            return this._context.Books
                .Include(b => b.Room)
                .Where(b => b.IsDeleted == 0 && (!b.IsRecurrence || b.RecurrenceNumber == 1))
                .OrderBy(b => b.BookDate)
                .ToList();
        }
        
        public IEnumerable<Book> GetBooksByUserId(string userId)
        {
            return this._context.Books
                .Include(b => b.Room)
                .Where(b => b.IsDeleted == 0 && (!b.IsRecurrence || b.RecurrenceNumber == 1) && b.UserId.Equals(userId))
                .OrderBy(b => b.BookDate)
                .ToList();
        }

        public IEnumerable<Book> GetCalendarBooks(string userId)
        {
            return this._context.Books
                .Include(b => b.Room)
                .Where(b => b.IsDeleted == 0 && b.UserId.Equals(userId))
                .OrderBy(b => b.BookDate)
                .ToList();
        }

        public void UpdateBookStatus(Book book)
        {
            this._context.Books.Update(book);
            this._context.SaveChanges();
        }

        public int CountBooking()
        {
            return this._context.Books.Count(b => b.IsDeleted == 0 && (!b.IsRecurrence || b.RecurrenceNumber == 1));
        }

        public int CountPendingBooking()
        {
            return this._context.Books.Count(b => b.IsDeleted == 0 && (!b.IsRecurrence || b.RecurrenceNumber == 1) && b.Status.Equals("Pending"));
        }

        public int CountAcceptedBooking()
        {
            return this._context.Books.Count(b => b.IsDeleted == 0 && (!b.IsRecurrence || b.RecurrenceNumber == 1) && b.Status.Equals("Accepted"));
        }

        public int CountRejectedBooking()
        {
            return this._context.Books.Count(b => b.IsDeleted == 0 && (!b.IsRecurrence || b.RecurrenceNumber == 1) && b.Status.Equals("Rejected"));
        }

        public BookObj MonthlyCountBooking()
        {
            // Query the database to group bookings by month and count them
            var monthlyCounts = _context.Books
                .Where(b => b.IsDeleted == 0) // Include only non-deleted bookings
                .GroupBy(b => b.CreatedDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Month, x => x.Count);

            // Populate BookObj with counts for each month
            var bookObj = new BookObj
            {
                JanBookingCount = monthlyCounts.ContainsKey(1) ? monthlyCounts[1] : 0,
                FebBookingCount = monthlyCounts.ContainsKey(2) ? monthlyCounts[2] : 0,
                MarBookingCount = monthlyCounts.ContainsKey(3) ? monthlyCounts[3] : 0,
                AprBookingCount = monthlyCounts.ContainsKey(4) ? monthlyCounts[4] : 0,
                MayBookingCount = monthlyCounts.ContainsKey(5) ? monthlyCounts[5] : 0,
                JunBookingCount = monthlyCounts.ContainsKey(6) ? monthlyCounts[6] : 0,
                JulBookingCount = monthlyCounts.ContainsKey(7) ? monthlyCounts[7] : 0,
                AugBookingCount = monthlyCounts.ContainsKey(8) ? monthlyCounts[8] : 0,
                SepBookingCount = monthlyCounts.ContainsKey(9) ? monthlyCounts[9] : 0,
                OctBookingCount = monthlyCounts.ContainsKey(10) ? monthlyCounts[10] : 0,
                NovBookingCount = monthlyCounts.ContainsKey(11) ? monthlyCounts[11] : 0,
                DecBookingCount = monthlyCounts.ContainsKey(12) ? monthlyCounts[12] : 0
            };

            return bookObj;
        }

        public IEnumerable<Book> GetFiveRecentlyPendingBooking(string userId)
        {
            return this._context.Books.Include(b => b.Room).Where(b => b.UserId.Equals(userId) && b.Status.Equals("Pending")).Take(5).ToList();
        }

        public IEnumerable<Book> GetFiveRecentlyAcceptedBooking(string userId)
        {
            return this._context.Books.Include(b => b.Room).Where(b => b.UserId.Equals(userId) && b.Status.Equals("Accepted")).Take(5).ToList();
        }

        public IEnumerable<Book> GetFiveUpComingBooking(string userId)
        {
            DateTime now = DateTime.Now;

            return this._context.Books.Include(b => b.Room)
                .Where(b => b.UserId.Equals(userId) &&  b.Status.Equals("Accepted") && b.BookDate > now.Date || (b.BookDate == now.Date && b.StartTime > now.TimeOfDay)) // Future bookings
                .OrderBy(b => b.BookDate)
                .ThenBy(b => b.StartTime) // Closest first
                .Take(5)
                .ToList();
        }
    }
}
