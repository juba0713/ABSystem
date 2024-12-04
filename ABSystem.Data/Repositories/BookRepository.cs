using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
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

        public BookRepository(ABSystemDbContext context)
        {
            _context = context;
        }

        /*
         * This method is for adding a booking to the database
         */
        public void AddBook(Book book)
        {
            this._context.Books.Add(book);
        }
    }
}
