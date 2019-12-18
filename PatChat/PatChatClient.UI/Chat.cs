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
using System.Threading;
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
        TcpClient clientSocket = new TcpClient();

        NetworkStream serverStream = default(NetworkStream);

        string readData = null;

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
        private void getMessage()

        {
            try
            {
            while (true)

            {
                serverStream = clientSocket.GetStream();

                int buffSize = 0;

                byte[] inStream = new byte[90025];

                buffSize = clientSocket.ReceiveBufferSize;

                serverStream.Read(inStream, 0, buffSize);

                string returndata = System.Text.Encoding.ASCII.GetString(inStream);

                readData =  returndata;

                msg();
              
              
                  //  PatChat.Entities.Entities.Message mess = Serialize.JsonDeserialize<PatChat.Entities.Entities.Message>(returndata);
                
                    // mess.Push();
                    //foreach (PatChat.Entities.Entities.Message item in mess)
                    //{
                    //    listBox1.Items.Add(_UserManager.GetUserById(item.UserId).Name + ":" + item.Content);
                    //}
                
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen sunucu yazılımı kapatıp yeniden açın!");
                this.Hide();
            }
        }
        int a = 0;
        private void msg()
          {

            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
            {
                if (readData!="OK")
                {
                    readData = readData.Substring(0, readData.IndexOf("$"));

                    if (readData.IndexOf("oky") ==-1)
                    {
                        PatChat.Entities.Entities.Message mess2 = Serialize.JsonDeserialize<PatChat.Entities.Entities.Message>(readData);
                        mess.Push(mess2);
                       

                       
                    
                        if (_GroupManager.IsExistsUserGroup(mess2.GroupId, Session.CurrentUser.Id))
                        {
                            listBox1.Items.Add(mess2.Id + "/" + _UserManager.GetUserById(mess2.UserId).Name + ":" + mess2.Content);
                        }
                       
                        



                    } 
                }
                else
                    label6.Text = "BAĞLANTI AKTİF ✓";
            }
        }
        void Connect()
        {
            readData = "OK";
            clientSocket.Connect("192.168.1.101", 8888);
            serverStream = clientSocket.GetStream();
            msg();
            string data = "";
            if (groupname==null)
            {
                data = Friend.Id;
            }
            else
            {
                data = groupname;
            }

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            Thread ctThread = new Thread(getMessage);

            ctThread.Start();
        }
      public  static string groupname { get; set; }
        private void Chat_Load(object sender, EventArgs e)
        {
            Connect();//Connect to server..


        
            if (groupname==null)
            {
                int Id = Convert.ToInt32(Session.CurrentUser.Id) - Convert.ToInt32(Friend.Id);
                if (Id < 0)
                    Id = Id * -1;
                group = "[" + (Id).ToString() + "]";
                string[] UserId = { Session.CurrentUser.Id, Friend.Id };
                if (!_GroupManager.IsExistsGroup(group))
                    _GroupManager.CreateGroup(group, UserId);

                this.Text = "[" + Session.CurrentUser.Name + "-" + Friend.Name + "]";
                label1.Text = "@" + Friend.UserName;
                label2.Text = Friend.Name;
                if (Friend.IsOnline)
                {
                    label3.Text = "AKTİF ✓";
                    label3.BackColor = Color.Green;
                }
                else { 
                    label3.Text = "Çevrimdışı";
               
                    label3.BackColor = Color.Red;

                }
                label4.Text = Friend.BirthDate.ToShortDateString().ToString();
            }
            else
            {
                group = groupname;
            }
          
          
            foreach (var item in _GroupManager.GroupList(group))
            {
                comboBox1.Items.Add(item.User.Name);
            }

            foreach (var item in _message.ListMessage(group))
            {
                listBox1.Items.Add(item.Id+"/"+_UserManager.GetUserById(item.UserId).Name + ":" + item.Content);
            }
        }
        void SendG(PatChat.Entities.Entities.Message message)
        {
            try
            {
                
                string data = Serialize.JsonSerializer<PatChat.Entities.Entities.Message>(message);

                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            }
            catch (Exception)
            {
                label6.Text = "Bağlantı yok!";
               
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        void senddx()
        {
            string rndstr = new Random().Next(0, 99999999).ToString();
            PatChat.Entities.Entities.Message message = new PatChat.Entities.Entities.Message
            {
                Content = textBox1.Text,
                IsDeleted = false,
                GroupId = _GroupManager.GetByGroupName(group).Id,
                UserId = Session.CurrentUser.Id,
                Id = rndstr
            };
            SendG(message);

            label7.Text = "İletildi ✓ [" + DateTime.Now.ToString("HH:mm") +"]" ;
            
            mess.Push(_UserManager.AddMessage(Session.CurrentUser, textBox1.Text, _GroupManager.GetByGroupName(group), rndstr));
            //listmessage();

            textBox1.Text = "";
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
                if (textBox1.Text.Length >= 2)
                    senddx();
                //send message tcp/ip
           
        }
        void listmessage()
        {
            foreach (PatChat.Entities.Entities.Message item in mess)
            {
                listBox1.Items.Add(_UserManager.GetUserById(item.UserId).Name + ":" + item.Content);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 2)
                senddx();
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            CreateGroup form = new CreateGroup();
            CreateGroup.group = _GroupManager.GetByGroupName(group);
            form.ShowDialog();
        }

        private void label8_MouseHover(object sender, EventArgs e)
        {
            label8.ForeColor= Color.LightGray;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string data = listBox1.SelectedItem.ToString();
            data = data.Substring(0, data.IndexOf(":"));
            string[] data2 = data.Split('/');
            LikeMessage likeMessage = new LikeMessage();
            LikeMessage.message = _message.GetUserById(data2[0]);
            likeMessage.ShowDialog();
        }
    }
}
