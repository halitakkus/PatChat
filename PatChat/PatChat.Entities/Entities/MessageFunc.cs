using System;
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
        public string Id { get; set; }
        public Message Message { get; set; }
        public string MessageId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public bool Func { get; set; }
    }
}
