using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProject
{
    public partial class ChatForm : Form
    {
        private Client client;
        public ChatForm(Client client)
        {
            this.client = client;
            this.client.MessageReceived += Client_MessageReceived;
            this.client.UsersListUpdate += Client_UsersListUpdate;
            this.client.SystemMessageReceived += Client_SystemMessageReceived;
            InitializeComponent();
        }

        private void Client_SystemMessageReceived(string message)
        {
            BeginInvoke(new Action(() =>
            {
                tbChat.AppendText("\r\n" + "[SYSTEM MESSAGE]: " + message);
            }), new object[0]);
        }

        private void Client_UsersListUpdate()
        {
            bool wasShowed = false;
            while (!wasShowed)
            {
                try
                {
                    BeginInvoke(new Action(() =>
                    {
                        listUsers.DataSource = client.ClientsNames;
                    }), new object[0]);
                    wasShowed = true;
                }
                catch (Exception) { }
            }
        }

        private void Client_MessageReceived(string fromWho, string message)
        {
            BeginInvoke(new Action(() =>
            {
                tbChat.AppendText("\r\n" + $"{fromWho}: " + message);
            }), new object[0]);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            client.SendMessage(tbMessage.Text);
            tbMessage.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            client.ExitMainChat();
        }

        private void btnPrivateChat_Click(object sender, EventArgs e)
        {
            client.StartPrivateChatWith(listUsers.SelectedItem.ToString());
        }
    }
}
