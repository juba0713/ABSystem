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
    }
}
