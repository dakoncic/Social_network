using ModelsLayer.DatabaseEntities;
using ModelsLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public void SendMessage(string currentUserId, string userName, string message)
        {
            _context.Messages.Add(new Message
            {
                UserId = currentUserId,
                Text = message,
                When = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0)
            });

            _context.SaveChanges();
        }
        public void AddNewHeartbeat(string currentUserId, string userName)
        {
            var onlineUser = _context.OnlineUsers.FirstOrDefault(a => a.Id == currentUserId);
            if (onlineUser == null)
            {
                _context.OnlineUsers.Add(new OnlineUser
                {
                    Id = currentUserId,
                    UserName = userName,
                    LastOnline = DateTime.Now
                });
            }
            else
            {
                onlineUser.LastOnline = DateTime.Now;
            }

            _context.SaveChanges();
        }

        //refaktorirat ovu metodu jer je užas
        public IEnumerable<OnlineUser> GetAllOnlineUsers()
        {
            var OnlineUsers = _context.OnlineUsers.ToList();
            var OnlineUsersUmodifiedCollection = new List<OnlineUser>();
            var AppUsers = new List<ApplicationUser>();
            foreach (var user in OnlineUsers)
            {
                if (user.LastOnline.Year != DateTime.Now.Year || user.LastOnline.Month != DateTime.Now.Month ||
                    user.LastOnline.Day != DateTime.Now.Day || user.LastOnline.Hour != DateTime.Now.Hour
                    || user.LastOnline.Minute != DateTime.Now.Minute ||
                    DateTime.Now.Second - user.LastOnline.Second > 10)
                // smanjiti broj sekundi na ~5
                {
                    _context.OnlineUsers.Remove(user);
                }
                else
                {
                    OnlineUsersUmodifiedCollection.Add(user);
                }
            }

            _context.SaveChanges();
            return OnlineUsersUmodifiedCollection;
        }
    }
}
