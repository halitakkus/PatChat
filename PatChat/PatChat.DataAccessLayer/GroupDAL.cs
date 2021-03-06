﻿using PatChat.Entities.Entities;
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
        public string GetGroupByName(string Name)
        {
            return Context.Groups.Where(i=>i.Name==Name).FirstOrDefault().Id;
        }
        public bool CreateGroup(string GroupName,string[] UserId)
        {
            if (Context.Groups.Any(i => i.Name == GroupName) == false)
            {
                Context.Groups.Add(new Group
                {
                    Id = Partner.CreateId(),
                    Name = GroupName
                });
                Context.SaveChanges();
            }
           
                if (!AddGroup(UserId, GroupName))
                    return false;
          
            return true;
        }
        public List<AddGroup> GroupList(string GroupName)
        {
            string GroupId = GetGroupByName(GroupName);
            return Context.AddGroups.Where(i => i.GroupId == GroupId).ToList();
        }
        public List<AddGroup> GroupList2(string UserId)
        {
            
            return Context.AddGroups.Where(i => i.UserId == UserId).ToList();
        }
        public bool AddGroup(string[] UserId,string GroupName)
        {
            string GroupId = GetGroupByName(GroupName);
            foreach (var item in UserId)
            {
                Context.AddGroups.Add(new AddGroup
                {
                    Id = Partner.CreateId(),
                    GroupId = GroupId,
                    UserId = item
                });
            }
            return Context.SaveChanges()>0;
        }

        public bool IsExistsGroup(string GroupName)
        {
            return Context.Groups.Any(i => i.Name==GroupName);
        }
        public bool IsExistsUserGroup(string GroupId, string UserName)
        {
            return Context.AddGroups.Any(i => i.UserId == UserName && i.GroupId== GroupId);
        }
        public Group GetByGroupName(string GroupName)
        {
            return Context.Groups.Where(i => i.Name == GroupName).FirstOrDefault();
        }

    }
}
