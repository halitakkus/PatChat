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

        public List<Message> ListMessage(string groupname)
        {
            var group = groupDAL.GetByGroupName(groupname);
            if (group is null)
                return null;

            return _message.ListMessage(group.Id);
        }
    }
}
