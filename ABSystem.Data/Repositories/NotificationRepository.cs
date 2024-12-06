﻿using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ABSystemDbContext _context;

        public NotificationRepository(ABSystemDbContext context)
        {
            _context = context;
        }

        public void AddNotification(Notification notification)
        {
            this._context.Notifications.Add(notification);
            this._context.SaveChanges();
        }

        public Notification GetNotificationByNotificationId(int notificationId)
        {
            return this._context.Notifications.Find(notificationId)!;
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return this._context.Notifications.ToList();
        }

        public IEnumerable<Notification> GetNotificationsByUserId(string userId)
        {
            return this._context.Notifications
                   .Where(notification => notification.UserId == userId) 
                   .OrderByDescending(notification => notification.CreatedDate)
                   .ToList(); 
        }

        public void UpdateNotificationRead(Notification notification)
        {
            this._context.Notifications.Update(notification);
            this._context.SaveChanges();
        }
    }
}
