using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Interfaces
{
    public interface INotificationRepository
    {
        /*
         * This method is to add a notification to the database
         */
        public void AddNotification(Notification notification);

        /*
         * This method is for getting all the notifications (Display in admin)
         */
        public IEnumerable<Notification> GetNotifications();

        /*
         * This method is for getting all the notifications by user id (Display in user)
         */
        public IEnumerable<Notification> GetNotificationsByUserId(string userId);

        /*
         * This method is to update notification read which means it has been read by the user
         */
        public void UpdateNotificationRead(Notification notification);

        /*
         * This method is to get notification by its id
         */
        public Notification GetNotificationByNotificationId(int notificationId);
    }
}
