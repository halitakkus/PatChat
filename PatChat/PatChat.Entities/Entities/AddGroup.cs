using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.Entities.Entities
{
  public  class AddGroup
    {
        [Key]
        public string Id { get; set; }
        public Group GroupId { get; set; }
        public User UserId { get; set; }
    }
}
