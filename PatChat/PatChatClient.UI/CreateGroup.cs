using PatChat.BusinessLayer;
using PatChat.Entities;
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
    public partial class CreateGroup : Form
    {
        GroupBLL _groupBLL { get; set; }
        UserBLL _UserManager { get; set; }
        public CreateGroup()
        {
            _groupBLL = new GroupBLL();
            _UserManager = new UserBLL();
            InitializeComponent();
        }
        public static Group group{get;set;}
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<User> user = _UserManager.Search(textBox1.Text);
            listBox1.DataSource = user;
            listBox1.DisplayMember = "UserName";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (_UserManager.IsExistsFriend())
            //{

            //}
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Gruba eklemek istiyormusunuz","Bilgi",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                string friendId = ((User)listBox1.SelectedItem).Id;
                string[] UserId = { friendId };
                if (!_UserManager.IsExistsFriend(friendId, Session.CurrentUser.Id))
            {
              DialogResult d =  MessageBox.Show("Bu kişi arkadaşınız değil! \n \n Yinede gruba eklemek istiyorsanız onaylayın.", "Uyarı!!", MessageBoxButtons.OKCancel);
                if (d==DialogResult.OK)
                {
                        
                        _groupBLL.CreateGroup(group.Name, UserId);
                        MessageBox.Show("Bu kişi artık bir katılımcı.");
                }
            }
            else
            {
                    _groupBLL.CreateGroup(group.Name, UserId);
                    MessageBox.Show("Bu kişi artık bir katılımcı.");
                }
            }
        }
    }
}
