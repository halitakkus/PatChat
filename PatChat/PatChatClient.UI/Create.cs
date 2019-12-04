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
    public partial class Create : Form
    {
        UserBLL _user;
        public Create()
        {
            _user = new UserBLL();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string createId = _user.CreateByStringId();
            User user = new User() { 
            Id = createId,
            UserName = textBox1.Text,
            Name = textBox2.Text,
            Password = textBox3.Text,
            BirthDate =Convert.ToDateTime( dateTimePicker1.Text),
            };
            if (_user.AddUser(user))
            {
                MessageBox.Show("Merhaba "+user.Name + ", kaydınız başarıyla gerçekleşti. Hemen giriş yapıp patchat mesajlaşmaya başlayabilirsiniz.","BAŞARILI!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Hide();
                return;
            }
            MessageBox.Show("Lütfen eksik alanları kontrol edin!!");
        }

        private void Create_Load(object sender, EventArgs e)
        {

        }

      
    }
}
