using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatChat.DataAccessLayer;
using PatChat.Entities.Entities;

namespace PatChat.BusinessLayer
{
    public class UserBLL
    {
        private UserDAL _UserDAL { get; set; }
       
        public UserBLL() {
            _UserDAL = new UserDAL();
        }
        public bool IsExists(string Id)
        {
            return _UserDAL.GetUserById(Id) != null;
        }
        public void SetOnline(string UserId)
        {
            _UserDAL.SetOnline(UserId);
        }
        public void SetOfline(string UserId)
        {
            _UserDAL.SetOfline(UserId);
        }
        public User IsExistsAndFind(string username,string password)
        {
            if (username == null || password == null)
                return null;
            return _UserDAL.IsExistsAndFind(username, password);
        }
        public bool RemoveFriend(string FriendId)
        {
            if (FriendId is null)
                return false;
            return _UserDAL.RemoveFriend(FriendId);
        }
        public List<AddFriend> ListFriends()
        {
            return _UserDAL.ListFriends();
        }
        public List<AddFriend> ListFriends(string FriendId)
        {
            return _UserDAL.ListFriends(FriendId);
        }
        public bool AddFriend(string FriendId)
        {
            if (FriendId is null)
                return false;
            return _UserDAL.AddFriend(FriendId);
        }

        public bool IsExistsFriend(string FriendId, string UserId)
        {
            if (FriendId == UserId)
                return true;
            if (_UserDAL.IsExistsFriend(FriendId,UserId))
                return true;
            return false;
        }
        public List<User> Search(string search)
        {
            return _UserDAL.Search(search) ;
        }
        public bool LoginControl(string username,string password)
        {
            return _UserDAL.IsExists(username,password);
        }
        public User GetUserById(string Id)
        {
            return _UserDAL.GetUserById(Id);
        }
        public bool RemoveById(string Id)
        {
            if (IsExists(Id))
            {
             var User = _UserDAL.GetUserById(Id);
             return   _UserDAL.Remove(User);
            }
            return false;
        }

        public string CreateByStringId()
        {
            return Partner.CreateId();
        }

        public bool AddUser(User user)
        {
            if (user == null||user.Name==""||user.Password=="")
                return false;

          return  _UserDAL.AddUser(user);
        }

        public Message AddMessage(User user,string message,Group group, string Id)
        {
            if (message is null)
                return null;

            return _UserDAL.AddMessage(message,group,  Id);
        }

       
    }
}
