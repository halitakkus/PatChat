using PatChat.Entities;
using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.DataAccessLayer
{
   public class FriendDetails
    {
        IFriendMethod _method;
      public FriendDetails(IFriendMethod method)
        {
            _method = method;
        }

        public bool Result(string FriendId,string Id)
        {
            return _method.Status(FriendId,Id);
        }


    }

    public interface IFriendMethod
    {
        bool Status(string FriendId, string Id);
    }
    public class MultiFriend: BaseContext, IFriendMethod
    {
        public bool Status(string FriendId, string Id)
        {
            Context.Users.Find(Session.CurrentUser.Id).addFriends.Add(new AddFriend{ 
            FriendId = FriendId,
            Id = Id
            });
            Context.Users.Find(FriendId).addFriends.Add(
                new AddFriend
                {
                    FriendId = Session.CurrentUser.Id,
                    Id = "_"+Id
                }
                );
            return Context.SaveChanges() > 0;
        }
    }
    public class SingleFriend : BaseContext, IFriendMethod
    {
        public bool Status(string FriendId, string Id)
        {
            Context.Users.Find(Session.CurrentUser.Id).addFriends.Add(new AddFriend()
            {
                FriendId = FriendId,
                Id = Id
            });
           return Context.SaveChanges() > 0;
        }
    }

}
