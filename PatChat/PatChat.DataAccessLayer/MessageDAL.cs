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
        void aa()
        {
           
        }
        public Message GetUserById(int Id)
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

    }
}
