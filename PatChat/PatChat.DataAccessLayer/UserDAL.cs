using PatChat.Entities;
using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.DataAccessLayer
{
   public class UserDAL : BaseContext
    {
      public bool AddUser(User user)
        {
            Context.Users.Add(user);
            return Context.SaveChanges() > 0;
        }
        public bool IsExists(string username,string password)
        {
            return Context.Users.Any(i => i.UserName == username && i.Password == password);
        }
        public User IsExistsAndFind(string username, string password)
        {
            return Context.Users.Where(i => i.UserName == username && i.Password == password).FirstOrDefault();
        }
        public List<AddFriend> ListFriends()
        {
            return Context.Users.Find(Session.CurrentUser.Id).addFriends.ToList();
        }
        public  List<AddFriend> ListFriends(string FriendId)
        {
            return Context.Users.Find(FriendId).addFriends.ToList();
        }

        public bool AddFriend(string FriendId)
        {
            string Id = Partner.CreateId();
            if (IsExistsFriend(FriendId, Id))
                return false;
            if (!Context.AddFriends.Any(i => i.Id == Id))
            {
                //Single or Multi add
                FriendDetails AddFriend = new FriendDetails(new SingleFriend());
                return AddFriend.Result(FriendId,Id);
            }
            else
                AddFriend(FriendId);

            return false;
        }

        public AddFriend FriendFind(string FriendId)
        {
            return Context.AddFriends.Where(i => i.FriendId == FriendId && i.User.Id == Session.CurrentUser.Id).FirstOrDefault();
        }
        public bool RemoveFriend(string FriendId)
        {
            if (FriendFind(FriendId) is null)
                return false;
            Context.AddFriends.Remove(FriendFind(FriendId));
           return Context.SaveChanges() > 0;
        }
        public bool IsExistsFriend(string FriendId,string UserId)
        {
            if (Context.AddFriends.Any(i => i.FriendId == FriendId && i.User.Id == UserId))
                return true;
            return false;
        }
        public Message AddMessage( string message,Group group )
        {
            string Id = Partner.CreateId();
            if (!Context.Messages.Any(i => i.Id == Id))
            {
             Context.Users.Find(Session.CurrentUser.Id).addMessage.Add(new Message()
                {
                    Content = message,
                    GroupId = group.Id,
                    IsDeleted = false,
                    Id = Id
                });
            }
            else
                AddMessage(message,group);

            if (Context.SaveChanges() > 0)
            {
                return Context.Messages.Find(Id);
            }
            else
                return null;
          
        }

        public User GetUserById(string Id)
        {
            return Context.Users.Find(Id);
        }
        public List<User> Search(string search)
        {
            return Context.Users.Where(i => i.Name.Contains(search) || i.UserName.Contains(search)).ToList();
        }
        public bool Remove(User user)
        {
            Context.Users.Remove(user);
            return Context.SaveChanges() > 0;
        }
    }
}
