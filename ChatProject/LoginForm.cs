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

namespace ChatProject
{
    public partial class LoginForm : Form
    {
        private Client client;
        private FormsManager manager;

        public LoginForm(Client client, FormsManager manager)
        {
            this.client = client;
            this.manager = manager;
            client.CurrentStatusReceived += Client_CurrentStatusReceived;
            client.LoginSuccess += Client_LoginSuccess;
            InitializeComponent();
        }
        
        private void Client_LoginSuccess()
        {
            manager.CreateChatForm(client);
            manager.CloseLoginForm();      
        }

        private void Client_CurrentStatusReceived(string obj)
        {
            BeginInvoke(new Action(() =>
            {
                tbStatus.AppendText("\r\n" + obj);
            }), new object[0]);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            client.RegisterLoginUser(Requests.Signup, tbNick.Text, tbPass.Text);
            client.Name = tbNick.Text;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client.RegisterLoginUser(Requests.Signin, tbNick.Text, tbPass.Text);
            client.Name = tbNick.Text;
        }
    }
}
