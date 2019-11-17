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
        public User IsExistsAndFind(string username,string password)
        {
            if (username == null || password == null)
                return null;
            return _UserDAL.IsExistsAndFind(username, password);
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

        public bool AddUser(User user)
        {
            if (user == null)
                return false;

          return  _UserDAL.AddUser(user);
        }

        public bool AddMessage(User user,string message,Group group)
        {
            if (user == null||message ==null)
                return false;

            return _UserDAL.AddMessage(user,message,group);
        }
    }
}
