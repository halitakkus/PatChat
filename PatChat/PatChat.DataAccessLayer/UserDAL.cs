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
        public User GetUserById(string Id)
        {
            return Context.Users.Find(Id);
        }
        public List<User> Search(string UName,string Name)
        {
            return Context.Users.Where(i => i.Name.Contains(Name) || i.UserName.Contains(UName)).ToList();
        }
        public bool Remove(User user)
        {
            Context.Users.Remove(user);
            return Context.SaveChanges() > 0;
        }
    }
}
