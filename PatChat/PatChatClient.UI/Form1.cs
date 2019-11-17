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
    public partial class Form1 : Form
    {
        UserBLL _UserManager { get; set; }
        GroupBLL _GroupManager { get; set; }

        public Form1()
        {
            _UserManager = new UserBLL();
            _GroupManager = new GroupBLL();

            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
         int Count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            _UserManager.AddUser(new User() { 
            Id = "aas",
            Name = "ergre",
            BirthDate = DateTime.Now,
            UserName = "gergre",
            Password = "gregre"
            });
            User user = _UserManager.GetUserById("0");
            Group g = _GroupManager.GetGroupById("1");
            _UserManager.AddMessage(user, "merhaba",g);
           */
            if (_UserManager.LoginControl(textBox1.Text,textBox2.Text))
            {
                Session.CurrentUser = _UserManager.IsExistsAndFind(textBox1.Text,textBox2.Text);
                if (Session.CurrentUser!=null)
                {
                    HomePage home = new HomePage();
                    home.Text = "Hoşgeldin " + Session.CurrentUser.Name;
                    home.Show();
                    this.Hide();
                }
            }
            else
            {
                Count++;
                this.Text = ":(";
                label4.Text = "Kullanıcı adı veya şifre hatalı!";
                label4.Text += " [" + Count + "]"; 
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Green;
            this.Text = ":)";
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
                label3.ForeColor = Color.Maroon;
            this.Text = ":(";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
