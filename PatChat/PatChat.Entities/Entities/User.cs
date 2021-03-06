﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.Entities.Entities
{
    public class User
    {
        public User()
        {
            addFriends = new List<AddFriend>();
            addGroups = new List<AddGroup>();
            addMessage = new List<Message>();
        }
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsOnline { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual List<Message> addMessage { get; set; }
        public virtual List<AddGroup> addGroups { get; set; }
        public virtual List<AddFriend> addFriends { get; set; }
    }
}
