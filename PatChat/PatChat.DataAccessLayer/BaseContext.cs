using PatChat.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.DataAccessLayer
{
    public class BaseContext
    {
        public  PatChatContext Context;
        public BaseContext()
        {
            Context = new PatChatContext();
        }

    }
}
