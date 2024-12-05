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
    }
}
