using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.DataAccessLayer
{
  public static class Partner
    {
        public static string CreateId()
        {
            Random rnd = new Random();
            return rnd.Next(0, 92245451).ToString();
        }

    }
}
