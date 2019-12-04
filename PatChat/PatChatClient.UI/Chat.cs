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
    public partial class Chat : Form
    {
        public static User Friend;
        UserBLL _UserManager;
        GroupBLL _GroupManager;
        string group;

        MessageList mess;
        public Chat()
        {
            mess =  new MessageList();
            _GroupManager = new GroupBLL();
            _UserManager = new UserBLL();
            InitializeComponent();
        }
       
        private void Chat_Load(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session.CurrentUser.Id) - Convert.ToInt32(Friend.Id);
            if (Id < 0)
                Id = Id*-1;
            group = "[" + (Id).ToString() + "]";
            string[] UserId = { Session.CurrentUser.Id, Friend.Id };
            if (!_GroupManager.IsExistsGroup(group))
                _GroupManager.CreateGroup(group, UserId);

            this.Text = "[" + Session.CurrentUser.Name + "-" + Friend.Name + "]";
            label1.Text = "@"+Friend.UserName;
            label2.Text = Friend.Name;
            if (Friend.IsOnline)
                label3.Text = "AKTİF";
            else
                label3.Text = "Çevrimdışı";
            label4.Text = Friend.BirthDate.ToShortDateString().ToString();

            foreach (var item in _GroupManager.GroupList(group))
                comboBox1.Items.Add(item.User.Name);
            comboBox1.Text = "Katılımcılar";
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            mess.Push(_UserManager.AddMessage(Session.CurrentUser, textBox1.Text, _GroupManager.GetByGroupName(group)));
            listBox1.Items.Clear();
            
            foreach (PatChat.Entities.Entities.Message item in mess)
            {
                listBox1.Items.Add(_UserManager.GetUserById(item.UserId).Name +":"+item.Content);
            }
            
        }
    }
}
