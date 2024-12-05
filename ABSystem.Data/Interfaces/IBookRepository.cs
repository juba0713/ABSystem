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

        /*
         * This method is for updating the booking status
         */
        public void UpdateBookStatus(Book book);

        /*
         * This method is for getting the book by its id
         */
        public Book GetBookById(int bookId);
    }
}
