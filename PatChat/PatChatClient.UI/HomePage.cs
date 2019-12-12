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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatChatClient.UI
{
    public partial class HomePage : Form
    {
        UserBLL _UserManager { get; set; }

       
        public HomePage()
        {
            _UserManager = new UserBLL();
            InitializeComponent();
        }
        void Loading()
        {
            groupBox1.Text = "Hoşgeldin " + Session.CurrentUser.Name;
            listView1.Visible = false;

            foreach (var item in _UserManager.ListFriends())
            {
                listBox1.Items.Add("@" + _UserManager.GetUserById(item.FriendId).UserName);
                listBox1.Tag = item.FriendId;
            }

            listBox1.DisplayMember = "UserId";
        }
        private void HomePage_Load(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(Loading)).Start();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            listView1.Visible = true;
            listView1.Items.Clear();
            List<User> user = _UserManager.Search(textBox1.Text);
            string[] row = new string[2];
            foreach (var item in user)
            {
                row[0] = "@"+item.UserName+" [" + item.Name+"]";
                row[1] = "+";
                ListViewItem r = new ListViewItem(row);
                r.Tag = item.Id;
                listView1.Items.Add(r);
                
            }
            //   l1.DataBindings.Add("Text", user, "Name");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DetailsForm.Friend = _UserManager.GetUserById(listView1.SelectedItems[0].Tag.ToString());
            DetailsForm form = new DetailsForm();
            form.Text = "detaylar";
            form.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session.CurrentUser = null;
            Form1 Login = new Form1();
            Login.Show();
            this.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DetailsForm.Friend = _UserManager.GetUserById(listBox1.Tag.ToString());
            DetailsForm details = new DetailsForm();
            details.Show();
        }
    }
}
