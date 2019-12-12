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
    public partial class Form1 : Form
    {
        UserBLL _UserManager { get; set; }
        GroupBLL _GroupManager { get; set; }
        HomePage home;
        public Form1()
        {
            _UserManager = new UserBLL();
            _GroupManager = new GroupBLL();
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
         int Count = 0;
        void Cont()
        {
            button1.Enabled = false;
            button1.Text = "Bekleyin..";
            if (_UserManager.LoginControl(textBox1.Text, textBox2.Text))
            {
                Session.CurrentUser = _UserManager.IsExistsAndFind(textBox1.Text, textBox2.Text);
                if (Session.CurrentUser != null)
                {
                    home = new HomePage();
                    if (this.InvokeRequired)
                    {
                        Action act = delegate {
                            home.Text = "PatChat mesenger V2.0";
                            home.Show();
                            this.Hide();
                        };
                        Invoke(act);
                    }else
                        home.Show();
                }
            }
            else
            {
                        label4.Text = "Kullanıcı adı veya şifre hatalı!";
                        label4.Text += " [" + Count++ + "]";
                        button1.Enabled = true;
                        button1.Text = "Giriş";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*
             User user = _UserManager.GetUserById("81841688");
             Group g = _GroupManager.GetGroupById("1");
             _UserManager.AddMessage(user, "merhaba",g);
             */
        
            Thread login = new Thread(new ThreadStart(Cont));
            login.Start();
            
          
          

        }





        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Create crt = new Create();
            crt.Text = "kayıt ekranı";
            crt.Show();
        }

       
    }
}
