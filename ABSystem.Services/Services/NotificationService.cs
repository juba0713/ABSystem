using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationService(INotificationRepository notificationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _notificationRepository = notificationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return this._notificationRepository.GetAllNotifications();
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return this._notificationRepository.GetNotifications();
        }

        public IEnumerable<Notification> GetNotificationsByUserId()
        {
            string loggedInUserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId")!;

            return this._notificationRepository.GetNotificationsByUserId(loggedInUserId);
        }

        public void UpdateNotificationRead(int notificationId)
        {
            var notification = this._notificationRepository.GetNotificationByNotificationId(notificationId);

            notification.IsRead = 1;
            notification.UpdateDate = DateTime.Now;

            this._notificationRepository.UpdateNotificationRead(notification);
        }

        public IEnumerable<Notification> GetRecentlyUserNotifications()
        {
            return this._notificationRepository.GetRecentlyUserNotifications();
        }
    }
}
