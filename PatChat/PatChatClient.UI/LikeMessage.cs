using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatChat.BusinessLayer;
using PatChat.Entities;
using PatChat.Entities.Entities;

namespace PatChatClient.UI
{
    public partial class LikeMessage : Form
    {
      public static  PatChat.Entities.Entities.Message message { get; set; }
        MessageBLL messagebll { get; set; }
        public LikeMessage()
        {
            messagebll = new MessageBLL();
            InitializeComponent();
        }

        private void LikeMessage_Load(object sender, EventArgs e)
        {
            label1.Text = message.Content;
            label2.Text = message.MessageFuncs.Where(i => i.Func == true).Count().ToString();
            label3.Text = message.MessageFuncs.Where(i => i.Func == false).Count().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(messagebll.LikesMessage(message.Id, Session.CurrentUser.Id, true))
            {
                label2.Text = (Convert.ToInt32(label2.Text) + 1).ToString();
                button1.Enabled = false;
                
                this.Text = "Mesajı beğendiniz.";
                listBox1.Items.Add(Session.CurrentUser.Name);
            }
            else
            {
                this.Text = "zaten beğenilmiş";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (messagebll.LikesMessage(message.Id, Session.CurrentUser.Id, false))
            {
                label3.Text = (Convert.ToInt32(label3.Text) + 1).ToString();
                this.Text = "Mesajı beğenmekten vazgeçtiniz.";
                listBox2.Items.Add(Session.CurrentUser.Name);
                button2.Enabled = false;


            }
            else
            {
                this.Text = "zaten beğenilmemiş";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Height = 400;
            button3.Enabled = false;
            foreach (var item in message.MessageFuncs.Where(i => i.Func == true).ToList())
            {
                listBox1.Items.Add(item.User.Name);
            }
            foreach (var item in message.MessageFuncs.Where(i => i.Func == false).ToList())
            {
                listBox2.Items.Add(item.User.Name);
            }

        }
    }
}
