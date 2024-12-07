using ABSystem.Data.Interfaces;
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

            int bookId = 0;

            if (!dto.IsRecurrence)
            {
                Book book = new Book();

                _mapper.Map(dto, book);
                book.Status = CommonConstant.PENDING;
                book.CreatedDate = DateTime.Now;
                book.UpdateDate = DateTime.Now;
                book.IsDeleted = 0;
                book.UserId = loggedInUserId;
                book.IsRecurrence = false;

                bookId = this._bookRepository.AddBook(book);
            }
            else
            {
                Guid guid = Guid.NewGuid();
                int sequence = 1;
                foreach(var date in dto.BookDates)
                {
                    Book book = new Book();

                    _mapper.Map(dto, book);
                    book.BookDate = date;
                    book.Status = CommonConstant.PENDING;
                    book.CreatedDate = DateTime.Now;
                    book.UpdateDate = DateTime.Now;
                    book.IsDeleted = 0;
                    book.UserId = loggedInUserId;
                    book.RecurrenceNumber = sequence;
                    book.RecurrenceType = dto.RecurrenceType;
                    book.IsRecurrence = true;
                    book.RecurrenceGroupId = guid.ToString();

                    bookId = this._bookRepository.AddBook(book);

                    sequence++;
                }
            }

            
            
            var room = this._roomRepository.GetRoomById(dto.RoomId);
         
            Notification notification = new Notification();

            DateTime startDateTime = DateTime.Today.Add(dto.StartTime);
            DateTime endDateTime = DateTime.Today.Add(dto.EndTime);

            //notification.UserId = loggedInUserId;
            notification.BookingId = bookId;
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

        public int CountAcceptedBooking()
        {
            return this._bookRepository.CountAcceptedBooking();

        }

        public int CountBooking()
        {
            return this._bookRepository.CountBooking();
        }

        public int CountPendingBooking()
        {
            return this._bookRepository.CountPendingBooking();
        }

        public int CountRejectedBooking()
        {
            return this._bookRepository.CountRejectedBooking();
        }

        public UserBookDto GetBookById(int bookId)
        {
            UserBookDto userBookDto = new UserBookDto();

            var book = this._bookRepository.GetBookById(bookId);

            _mapper.Map(book, userBookDto);

            return userBookDto;
        }

        public IEnumerable<Book> GetBooks()
        {
            return this._bookRepository.GetBooks();
        }

        public IEnumerable<UserBookDto> GetBooksByUserId()
        {
            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId")!;

            if (string.IsNullOrEmpty(loggedInUserId))
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            List<UserBookDto> listBooks = new List<UserBookDto>();

            var books = this._bookRepository.GetBooksByUserId(loggedInUserId);

            foreach (var book in books)
            {
                UserBookDto userBookDto = new UserBookDto();
                _mapper.Map(book, userBookDto);
                listBooks.Add(userBookDto);
            }

            return listBooks;
        }

        public IEnumerable<UserBookDto> GetCalendarBooks()
        {
            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId")!;

            if (string.IsNullOrEmpty(loggedInUserId))
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            List<UserBookDto> listBooks = new List<UserBookDto>();

            var books = this._bookRepository.GetCalendarBooks(loggedInUserId);

            foreach (var book in books)
            {
                UserBookDto userBookDto = new UserBookDto();
                _mapper.Map(book, userBookDto);
                listBooks.Add(userBookDto);
            }

            return listBooks;
        }

        public IEnumerable<UserBookDto> GetFiveRecentlyAcceptedBooking()
        {

            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId")!;

            if (string.IsNullOrEmpty(loggedInUserId))
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            List<UserBookDto> listBooks = new List<UserBookDto>();

            var books = this._bookRepository.GetFiveRecentlyAcceptedBooking(loggedInUserId);

            foreach (var book in books)
            {
                UserBookDto userBookDto = new UserBookDto();
                _mapper.Map(book, userBookDto);
                listBooks.Add(userBookDto);
            }

            return listBooks;

        }

        public IEnumerable<UserBookDto> GetFiveRecentlyPendingBooking()
        {
            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId")!;

            if (string.IsNullOrEmpty(loggedInUserId))
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            List<UserBookDto> listBooks = new List<UserBookDto>();

            var books = this._bookRepository.GetFiveRecentlyPendingBooking(loggedInUserId);

            foreach (var book in books)
            {
                UserBookDto userBookDto = new UserBookDto();
                _mapper.Map(book, userBookDto);
                listBooks.Add(userBookDto);
            }

            return listBooks;
        }

        public IEnumerable<UserBookDto> GetFiveUpComingBooking()
        {

            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId")!;

            if (string.IsNullOrEmpty(loggedInUserId))
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            List<UserBookDto> listBooks = new List<UserBookDto>();

            var books = this._bookRepository.GetFiveUpComingBooking(loggedInUserId);

            foreach (var book in books)
            {
                UserBookDto userBookDto = new UserBookDto();
                _mapper.Map(book, userBookDto);
                listBooks.Add(userBookDto);
            }

            return listBooks;
        }

        public BookObj MonthlyCountBooking()
        {
            return this._bookRepository.MonthlyCountBooking();
        }

        public void UpdateBookStatus(int bookId, string status)
        {
            var book = this._bookRepository.GetBookById(bookId);

            book.Status = status;
            book.UpdateDate = DateTime.Now;

            this._bookRepository.UpdateBookStatus(book);

            var room = this._roomRepository.GetRoomById(book.RoomId);

            Notification notification = new Notification();

            DateTime startDateTime = DateTime.Today.Add(book.StartTime);
            DateTime endDateTime = DateTime.Today.Add(book.EndTime);

            notification.UserId = book.UserId;
            notification.BookingId = bookId;
            notification.RoomId = room.Id;
          
            notification.CreatedDate = DateTime.Now;
            notification.UpdateDate = DateTime.Now;
            notification.IsRead = 0;
            notification.IsDeleted = 0;

            if (status.Equals(CommonConstant.ACCEPTED))
            {
                notification.Message = "Your booking for " +
                       room.Name + " on " +
                       book.BookDate.ToString("MMM dd, yyyy") + " from " +
                       startDateTime.ToString("hh:mm tt") + " to " +
                       endDateTime.ToString("hh:mm tt") +
                       " has been confirmed by the admin.";
            }else if (status.Equals(CommonConstant.REJECTED))
            {
                notification.Message = "Your booking for " +
                       room.Name + " on " +
                       book.BookDate.ToString("MMM dd, yyyy") + " from " +
                       startDateTime.ToString("hh:mm tt") + " to " +
                       endDateTime.ToString("hh:mm tt") +
                       " has been rejected by the admin.";
            }else if (status.Equals(CommonConstant.CANCELED))
            {
                notification.UserId = "";
                notification.Message = book.FirstName + " " + book.LastName +"'s booking for " +
                       room.Name + " on " +
                       book.BookDate.ToString("MMM dd, yyyy") + " from " +
                       startDateTime.ToString("hh:mm tt") + " to " +
                       endDateTime.ToString("hh:mm tt") +
                       " has been canceled.";
            }

            this._notificationRepository.AddNotification(notification);
        }
    }
}
