using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.DataAccessLayer
{
   public class MessageDAL : BaseContext
    {
       public List<Message> ListMessage(string groupId)
        {
            return Context.Messages.Where(i=>i.GroupId== groupId).ToList();
        }
        public Message GetUserById(string Id)
        {
            return Context.Messages.Find(Id);
        }
        public List<Message> Search(string Message, string GroupId)
        {
            return Context.Messages.Where(i => i.Content.Contains(Message) || i.Group.Id==GroupId).ToList();
        }
        public bool Remove(Message message)
        {
            Context.Messages.Remove(message);
            return Context.SaveChanges() > 0;
        }

        public bool LikesMessage(string MessageId,string UserId,bool stat)
        {
            if (Context.MessageFuncs.Any(i => i.UserId == UserId && i.MessageId == MessageId) == false)
                Context.MessageFuncs.Add(new MessageFunc()
                {
                    Id = Partner.CreateId(),
                    Func = stat,
                    MessageId = MessageId,
                    UserId = UserId
                });
            else
                Context.MessageFuncs.Where(i => i.UserId == UserId && i.MessageId == MessageId).FirstOrDefault().Func = stat;
            return Context.SaveChanges() > 0;
        }

    }
}
