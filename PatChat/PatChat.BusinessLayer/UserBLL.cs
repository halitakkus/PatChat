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

        public bool RemoveById(string Id)
        {
            if (IsExists(Id))
            {
             var User = _UserDAL.GetUserById(Id);
             return   _UserDAL.Remove(User);
            }
            return false;
        }

        public bool AddUser(User user)
        {
            if (user == null)
                return false;

          return  _UserDAL.AddUser(user);
        }
    }
}
