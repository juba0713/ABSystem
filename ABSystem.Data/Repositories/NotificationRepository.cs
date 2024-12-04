using ABSystem.Data.Interfaces;
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

        public void AddNotification()
        {

        }
    }
}
