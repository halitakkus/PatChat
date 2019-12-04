using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.BusinessLayer
{
   public class FriendStatus
    {
        private IFriendStatus _status;
        public FriendStatus(IFriendStatus status)
        {
            _status = status;
        }
        public string Set(string friendId)
        {
           return  _status.Status(friendId);
        }
    }

    public interface IFriendStatus
    {
        string Status(string friendId);
    }

  public  class FriendIsExists : IFriendStatus
    {
        UserBLL _user;
        public FriendIsExists()
        {
            _user = new UserBLL();
        }
        public string Status(string friendId)
        {
            if (_user.RemoveFriend(friendId))
                return "Arkadaşlardan çıkartıldı";
            return "Bir sorun oluştu.";
        }
    }
    public class FriendIsNotExists : IFriendStatus
    {
        UserBLL _user;
        public FriendIsNotExists()
        {
            _user = new UserBLL();
        }
        public string Status(string friendId)
        {
            if (_user.AddFriend(friendId))
                return "Arkadaş eklendi";
            return "Bir sorun oluştu.";
        }
    }
}
