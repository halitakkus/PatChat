﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.Entities.Entities
{
   public class AddFriend
    {
        [Key]
        public  string Id { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public bool CloseView { get; set; }
    }
}
