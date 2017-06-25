using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProject
{
    public partial class MainForm : Form
    {
        private FormsManager manager;
        public MainForm(FormsManager manager)
        {
            this.manager = manager;
            InitializeComponent();
        }

        private void BtnStartServer_Click(object sender, EventArgs e)
        {
            this.Hide();
            Server server = new Server(IPAddress.Parse(tbIp.Text));
            ServerForm form = new ServerForm(server);
            server.Start();
            form.ShowDialog();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.Start(IPAddress.Parse(tbIp.Text), manager);
            manager.CreateLoginForm(client, this);
        }

        //private void btnRegister_Click(object sender, EventArgs e)
        //{

        //}
    }
}
