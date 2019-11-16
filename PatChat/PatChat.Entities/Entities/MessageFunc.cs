﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.Entities.Entities
{
   public class MessageFunc
    {
        [Key]
        public int Id { get; set; }
        public Message Message { get; set; }
        public User User { get; set; }
        public bool Func { get; set; }
    }
}