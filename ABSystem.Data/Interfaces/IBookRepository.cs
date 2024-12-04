using ABSystem.Data.Models;
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
        public void AddBook(Book book);
    }
}
