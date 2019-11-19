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
    public partial class HomePage : Form
    {
        UserBLL _UserManager { get; set; }

        ListBox l1 { get; set; }
        public HomePage()
        {
            _UserManager = new UserBLL();
            l1 = new ListBox();
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            groupBox1.Text = "Hoşgeldin " + Session.CurrentUser.Name;
            l1.Location = new Point(0, 0);
            l1.Text = "ara";
            l1.Width = 400;
            l1.Height = 500;
            l1.Visible = false;
            panel1.Controls.Add(l1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            l1.Visible = true;
            List<User> user = _UserManager.Search(textBox1.Text);
            l1.DataSource = user;
          //   l1.DataBindings.Add("Text", user, "Name");

        }
    }
}
