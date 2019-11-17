using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.DataAccessLayer
{
  public  class GroupDAL : BaseContext
    {
        public Group GetGroupById(string Id)
        {
            return Context.Groups.Find(Id);
        }

    }
}
