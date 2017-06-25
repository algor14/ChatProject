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
    public partial class ServerForm : Form
    {
        private Server server;

        public ServerForm(Server server)
        {
            this.server = server;
            this.server.LoggingUpdate += Server_LoggingUpdate;
            this.server.UsersListUpdate += Server_UsersListUpdate;
            InitializeComponent();
        }

        private void Server_UsersListUpdate(string obj)
        {
            BeginInvoke(new Action(() =>
            {
                listUsers.DataSource = server.ClientsNames;
            }), new object[0]);
        }

        private void Server_LoggingUpdate(string obj)
        {
            BeginInvoke(new Action(() =>
            {
                tbLogging.AppendText("\r\n" + obj);               
            }), new object[0]);
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            server.Shutdown();
        }
    }
}
