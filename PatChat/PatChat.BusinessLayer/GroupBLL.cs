using PatChat.DataAccessLayer;
using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.BusinessLayer
{
   public class GroupBLL
    {
        GroupDAL _Group { get; set; }
       public GroupBLL()
        {
            _Group = new GroupDAL();
        }
        public Group GetGroupById(string Id)
        {
            return _Group.GetGroupById(Id);
        }
        public bool IsExistsGroup(string GroupName)
        {
            return _Group.IsExistsGroup(GroupName);
        }
        public bool CreateGroup(string GroupName,string[] UserId)
        {
            return _Group.CreateGroup(GroupName,UserId);
        }

        public Group GetByGroupName(string GroupName)
        {
            return _Group.GetByGroupName(GroupName);
        }
        public List<AddGroup> GroupList(string GroupName)
        {
            return    _Group.GroupList(GroupName).ToList();
        }

    }
}
