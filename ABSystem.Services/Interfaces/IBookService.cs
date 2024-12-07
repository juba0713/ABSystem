using ABSystem.Data.Models;
using ABSystem.Data.Objects;
using ABSystem.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Interfaces
{
    public interface IBookService
    {
        /*
         * This method is for adding a booking to the database
         */
        public void AddBook(UserBookDto dto);

        /*
         * This method is for getting all the booking
         */
        public IEnumerable<Book> GetBooks();

        /*
         * This method is for updating the booking status
         */
        public void UpdateBookStatus(int bookId, string status);

        /*
         * This method is for getting the book by its id
         */
        public UserBookDto GetBookById(int bookId);

        /*
         * This method is for getting books by user id
         */
        public IEnumerable<UserBookDto> GetBooksByUserId();

        /*
         * This method is for getting books by user id
         */
        public IEnumerable<UserBookDto> GetCalendarBooks();

        public int CountBooking();

        public int CountPendingBooking();

        public int CountAcceptedBooking();

        public int CountRejectedBooking();
        public BookObj MonthlyCountBooking();

        public IEnumerable<UserBookDto> GetFiveRecentlyPendingBooking();
        public IEnumerable<UserBookDto> GetFiveRecentlyAcceptedBooking();
        public IEnumerable<UserBookDto> GetFiveUpComingBooking();
    }
}
