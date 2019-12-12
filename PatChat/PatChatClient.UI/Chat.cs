using Newtonsoft.Json;
using PatChat.BusinessLayer;
using PatChat.Entities;
using PatChat.Entities.Entities;
using PatChat.UIObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatChatClient.UI
{
    public partial class Chat : Form
    {
        public static User Friend;
        UserBLL _UserManager;
        MessageBLL _message;
        GroupBLL _GroupManager;
        string group;
        //
        public TcpClient client;
        private NetworkStream networkstream;
        private StreamReader networkreader;
        private StreamWriter writer;
        //
        MessageList mess;
        public Chat()
        {
            _message = new MessageBLL();
            mess =  new MessageList();
            _GroupManager = new GroupBLL();
            _UserManager = new UserBLL();
            InitializeComponent();
        }
       
        private void Chat_Load(object sender, EventArgs e)
        {
           
            client = new TcpClient("localhost", 1234);

            networkstream = client.GetStream();
            networkreader = new StreamReader(networkstream);
            writer = new StreamWriter(networkstream);
            //
            
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
            {
                comboBox1.Items.Add(item.User.Name);
                comboBox1.Text = "Katılımcılar";
            }
            foreach (var item in _message.ListMessage(group))
            {
                listBox1.Items.Add(_UserManager.GetUserById(item.UserId).Name + ":" + item.Content);
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //send message tcp/ip
            PatChat.Entities.Entities.Message message = new PatChat.Entities.Entities.Message
            {
                Content = textBox1.Text,
                IsDeleted = false,
                GroupId = _GroupManager.GetByGroupName(group).Id,
                UserId = Session.CurrentUser.Id
            };
            string yazi = Serialize.JsonSerializer<PatChat.Entities.Entities.Message>(message);
            writer.WriteLine(yazi);
            this.Text = "İletiliyor..";
            writer.Flush();
            
            if (networkreader.ReadLine().Length>0)
            {
                this.Text = "İletildi.";
                mess.Push(_UserManager.AddMessage(Session.CurrentUser, textBox1.Text, _GroupManager.GetByGroupName(group)));
                foreach (PatChat.Entities.Entities.Message item in mess)
                {
                    listBox1.Items.Add(_UserManager.GetUserById(item.UserId).Name + ":" + item.Content);
                }
            }
          
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
