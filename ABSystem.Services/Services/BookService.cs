﻿using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Data.Objects;
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
        private readonly IRoomRepository _roomRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(IBookRepository bookRepository, 
            IMapper mapper, 
            IRoomRepository roomRepository, 
            IHttpContextAccessor httpContextAccessor,
            INotificationRepository notificationRepository) {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _roomRepository = roomRepository;
            _httpContextAccessor = httpContextAccessor;
            _notificationRepository = notificationRepository;
        }

        public void AddBook(UserBookDto dto)
        {
            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId")!;
            string loggedInUserFullName = _httpContextAccessor.HttpContext?.Session.GetString("UserFullName")!;

            if (string.IsNullOrEmpty(loggedInUserId) || string.IsNullOrEmpty(loggedInUserFullName))
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

            var room = this._roomRepository.GetRoomById(dto.RoomId);

            Notification notification = new Notification();

            DateTime startDateTime = DateTime.Today.Add(dto.StartTime);
            DateTime endDateTime = DateTime.Today.Add(dto.EndTime);

            notification.UserId = loggedInUserId;
            notification.RoomId = room.Id;
            notification.Message = loggedInUserFullName + " has booked " +
                                   room.Name + " on " +
                                   dto.BookDate.ToString("MMM dd, yyyy") + " from " +
                                   startDateTime.ToString("hh:mm tt") + " to " +
                                   endDateTime.ToString("hh:mm tt");

            notification.CreatedDate = DateTime.Now;
            notification.UpdateDate = DateTime.Now;
            notification.IsRead = 0;
            notification.IsDeleted = 0;

            this._notificationRepository.AddNotification(notification);

        }

        public IEnumerable<Book> GetBooks()
        {
            return this._bookRepository.GetBooks();
        }

        public void UpdateBookStatus(int bookId, string status)
        {
            var book = this._bookRepository.GetBookById(bookId);

            book.Status = status;
            book.UpdateDate = DateTime.Now;

            this._bookRepository.UpdateBookStatus(book);
        }
    }
}
