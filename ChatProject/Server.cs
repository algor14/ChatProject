using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatProject
{
    public class Server
    {
        private Dictionary<string, string> Users;
        private TcpListener listener;
        private List<ConnectedClient> clients = new List<ConnectedClient>();
        public List<string> ClientsNames = new List<string>();
        public event Action<string> LoggingUpdate;
        public event Action<string> UsersListUpdate;

        public Server(IPAddress address)
        {
            listener = new TcpListener(address, 8080);
            LoadCredentials();
        }

        public void LoadCredentials()
        {
            Users = new Dictionary<string, string>();
            string[] substr;
            if (File.Exists("users.txt"))
            {
                substr = File.ReadAllLines("users.txt");
                foreach (var str in substr)
                {
                    string[] substr2 = str.Split(':');
                    Users[substr2[0]] = substr2[1];
                }
                Logging("Users DB loaded");
            }
        }

        public IEnumerable<ConnectedClient> Clients
        {
            get
            {
                return clients;
            }
        }

        public async void Start()
        {
            listener.Start();
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                ConnectedClient newClient = new ConnectedClient(this, client);
                Logging("New client is connecting...");
                newClient.Start();
                clients.Add(newClient);
            }
        }

        public void UpdateUsersList()
        {
            ClientsNames = new List<string>();
            foreach (var c in clients)
                ClientsNames.Add(c.Name);
            UsersListUpdate?.Invoke("User list update");
            foreach (var cl in clients)
                cl.SendUsersList();
        }

        public void Shutdown()
        {
            for (int i = 0; i < clients.Count; i++)
                DeleteConnectedClient(clients[i]);
            Logging("Server was shutted down");
            System.Windows.Forms.Application.Exit();
        }

        public void DeleteConnectedClient(ConnectedClient connectedClient)
        {
            Logging($"{connectedClient.Name} went out");
            connectedClient.CloseConnectedClient();
            ClientsNames.Remove(connectedClient.Name);
            clients.Remove(connectedClient);
            UpdateUsersList();
        }

        public void RegisterNewUser(string login, string pass)
        {
            Users[login] = pass;
            string[] substr = new string[Users.Count];
            int i = 0;
            foreach (var user in Users)
                substr[i++] = user.Key + ":" + user.Value;
            File.WriteAllLines("users.txt", substr);
        }

        public bool CheckUser(string login, string pass)
        {
            if (Users.ContainsKey(login))
                if (Users[login] == pass)
                    return true;
                else
                    return false;
            else
                return false;
        }

        public void Logging(string message)
        {
            LoggingUpdate?.Invoke(message);
        }
    }
}
