using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.DataAccessLayer.Context
{
    public class PatChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<AddGroup> AddGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageFunc> MessageFuncs { get; set; }
        public DbSet<AddFriend> AddFriends { get; set; }
    }
}
