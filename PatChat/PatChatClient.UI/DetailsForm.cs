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
    public partial class DetailsForm : Form
    {
        public static User Friend;
        UserBLL _user;
        public DetailsForm()
        {
            _user = new UserBLL();
            InitializeComponent();
        }
       
        string UserDetail()
        {
           return this.Text = "@"+Friend.UserName + " [" + Friend.Name + "]"; 
        }
        private void DetailsForm_Load(object sender, EventArgs e)
        {
            if (_user.IsExistsFriend(Friend.Id, Session.CurrentUser.Id))
                button2.Text = "Arkadaşlardan çıkar!";
            this.Text = UserDetail();
            label1.Text = UserDetail();
            label2.Text = "Doğum tarihi: " + Friend.BirthDate.ToShortDateString().ToString();
            label3.Text = "Arkadaş sayısı:-";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            FriendStatus friend;
            if (_user.IsExistsFriend(Friend.Id,Session.CurrentUser.Id))
                friend = new FriendStatus(new FriendIsExists());
            else
                friend = new FriendStatus(new FriendIsNotExists());
            button2.Text = friend.Set(Friend.Id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FriendList.Friend = _user.GetUserById(Friend.Id);
            FriendList friends = new FriendList();
            friends.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Chat chat = new Chat();
            Chat.Friend = _user.GetUserById(Friend.Id);
            chat.Show();
            this.Hide();
        }
    }
}
