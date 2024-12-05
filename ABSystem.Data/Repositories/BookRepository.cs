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
                .ToList();
        }

        public void UpdateBookStatus(Book book)
        {
            this._context.Books.Update(book);
            this._context.SaveChanges();
        }
    }
}
