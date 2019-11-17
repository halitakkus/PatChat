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
            addGroups = new List<AddGroup>();
        }
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Message> addMessage { get; set; }

        public List<AddGroup> addGroups { get; set; }
    }
}
