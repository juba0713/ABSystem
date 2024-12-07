using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Interfaces
{
    public interface INotificationService
    {
        /*
         * This method is for getting all the notifications (Display in admin)
         */
        public IEnumerable<Notification> GetNotifications();

        /*
         * This method is for getting all the notifications by user Id (Display in user)
         */
        public IEnumerable<Notification> GetNotificationsByUserId();

        /*
         * This method is to update notification read which means it has been read by the user
         */
        public void UpdateNotificationRead(int notificationId);

        public IEnumerable<Notification> GetAllNotifications();

        public IEnumerable<Notification> GetRecentlyUserNotifications();
    }
}
