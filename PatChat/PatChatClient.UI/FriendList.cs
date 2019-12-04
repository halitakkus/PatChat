using PatChat.BusinessLayer;
using PatChat.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatChatClient.UI
{
    public partial class FriendList : Form
    {
        public static User Friend;
         UserBLL _UserManager;
        public FriendList()
        {
            _UserManager = new UserBLL();
            InitializeComponent();
        }

        private void FriendList_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string[] row = new string[2];
            
            foreach (var item in _UserManager.ListFriends(Friend.Id))
            {
                row[0] =  _UserManager.GetUserById(item.FriendId).Id;
                row[1] = "@" + _UserManager.GetUserById(item.FriendId).UserName;
                ListViewItem r = new ListViewItem(row);
                r.Tag = item.FriendId;
                listView1.Items.Add(r);
            }
        }
    }
}
