using PatChat.DataAccessLayer;
using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.BusinessLayer
{
   public class MessageBLL
    {
        MessageDAL _message { get; set; }
        GroupDAL groupDAL { get; set; }
      public  MessageBLL()
        {
            groupDAL = new GroupDAL();
            _message = new MessageDAL();
        }
        public Message GetUserById(string Id)
        {
            return _message.GetUserById(Id);
        }
        public List<Message> ListMessage(string groupname)
        {
            var group = groupDAL.GetByGroupName(groupname);
            if (group is null)
                return null;

            return _message.ListMessage(group.Id);
        }


        public bool LikesMessage(string MessageId, string UserId, bool stat)
        {
            return _message.LikesMessage( MessageId,  UserId,  stat);
        }
    }
}
