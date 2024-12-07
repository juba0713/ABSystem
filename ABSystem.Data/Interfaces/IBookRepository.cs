using ABSystem.Data.Models;
using ABSystem.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Interfaces
{
    public interface IBookRepository
    {
        /*
         * This method is for adding a booking to the database
         */
        public int AddBook(Book book);

        /*
         * This method is for getting all the booking
         */
        public IEnumerable<Book> GetBooks();

        public IEnumerable<Book> GetCalendarBooks(string userId);

        /*
         * This method is for updating the booking status
         */
        public void UpdateBookStatus(Book book);

        /*
         * This method is for getting the book by its id
         */
        public Book GetBookById(int bookId);

        /*
         * This method is for getting books by user id
         */
        public IEnumerable<Book> GetBooksByUserId(string userId);

        public int CountBooking();

        public int CountPendingBooking();

        public int CountAcceptedBooking();

        public int CountRejectedBooking();

        public BookObj MonthlyCountBooking();

        public IEnumerable<Book> GetFiveRecentlyPendingBooking(string userId);
        public IEnumerable<Book> GetFiveRecentlyAcceptedBooking(string userId);
        public IEnumerable<Book> GetFiveUpComingBooking(string userId);
    }
}
