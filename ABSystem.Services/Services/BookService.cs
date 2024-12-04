using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(IBookRepository bookRepository, IMapper mapper, IUserService userService, IHttpContextAccessor httpContextAccessor) {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddBook(UserBookDto dto)
        {
            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (string.IsNullOrEmpty(loggedInUserId))
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            Book book = new Book();

            _mapper.Map(dto, book);
            book.Status = CommonConstant.PENDING;
            book.CreatedDate = DateTime.Now;
            book.UpdateDate = DateTime.Now;
            book.IsDeleted = 0;
            book.UserId = loggedInUserId;

            this._bookRepository.AddBook(book);

            Notification notification = new Notification();


        }
    }
}
