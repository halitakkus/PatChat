using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.Entities.Entities
{ 
   public class Message
    {
        [Key]
        public string Id { get; set; }
        public virtual Group Group { get; set; }
        public string GroupId { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<MessageFunc> MessageFuncs { get; set; }
    }
}
