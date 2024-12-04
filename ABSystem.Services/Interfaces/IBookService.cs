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
        public void AddBook(UserBookDto dto);
    }
}
