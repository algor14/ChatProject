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
    public partial class PrivateChatForm : Form
    {
        private string interlocutor;
        private Client client;
        public PrivateChatForm(Client client, string interlocutor)
        {
            this.client = client;
            this.interlocutor = interlocutor;
            this.client.PrivateMessageReceived += Client_MessageReceived;
            InitializeComponent();
            lbInterlocutor.Text = interlocutor;
        }

        private void Client_MessageReceived(string fromWho, string message)
        {
            if (interlocutor != fromWho)
                return;
            BeginInvoke(new Action(() =>
            {
                tbChat.AppendText("\r\n" + $"{fromWho}: " + message);
            }), new object[0]);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            client.SendMessage(tbMessage.Text, interlocutor);
            BeginInvoke(new Action(() =>
            {
                tbChat.AppendText("\r\n" + $"{client.Name}: " + tbMessage.Text);
                tbMessage.Text = "";
            }), new object[0]);           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            client.ExitPrivateChat(interlocutor);
            Close();
        }
    }
}
