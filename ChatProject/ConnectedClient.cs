using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatProject
{
    public class ConnectedClient
    {
        private TcpClient client;
        private Server server;
        private BinaryWriter writer;
        private BinaryReader reader;
        private bool isRunning = true;
        public string Name { get; set; }

        public ConnectedClient(Server server, TcpClient client)
        {
            this.client = client;
            this.server = server;
        }

        public void Start()
        {
            Thread th = new Thread(() => ClientProc(client));
            th.IsBackground = true;
            th.Start();
        }  

        private void ClientProc(TcpClient client)
        {
            using (client)
            {
                using (var stream = client.GetStream())
                {
                    writer = new BinaryWriter(stream);
                    reader = new BinaryReader(stream);
                    writer.Write((int)Requests.ConnectionOK);
                    writer.Flush();
                    while (isRunning)
                    {
                        switch ((Requests)reader.ReadInt32())
                        {
                            case Requests.Message:
                                string message = reader.ReadString();
                                foreach (var c in server.Clients)
                                    c.SendMessage(Name, message);
                                break;
                            case Requests.PrivateMessage:
                                string interlocutor = reader.ReadString();
                                message = reader.ReadString();
                                GetConnectedClientByName(interlocutor).SendPrivateMessage(Name, message);
                                break;
                            case Requests.Signup:
                                string[] credentialsStrings = reader.ReadString().Split(':');
                                SignUp(credentialsStrings[0], credentialsStrings[1]);
                                break;
                            case Requests.Signin:
                                credentialsStrings = reader.ReadString().Split(':');
                                SignIn(credentialsStrings[0], credentialsStrings[1]);
                                break;
                            case Requests.ExitMainChat:
                                server.DeleteConnectedClient(this);
                                break;
                            case Requests.StartPrivateChat:
                                string interlocutorName = reader.ReadString();
                                StartPrivateChatBetween(Name, interlocutorName);
                                break;
                            case Requests.EndPrivateChat:
                                interlocutorName = reader.ReadString();
                                StopPrivateChat(Name, interlocutorName);
                                break;
                        }
                    }
                }
            }
        }

        public void CloseConnectedClient()
        {
            isRunning = false;
        }

        public void OpenPrivateChatForm(string interlocutor)
        {
            writer.Write((int)Requests.StartPrivateChat);
            writer.Write(interlocutor);
            writer.Flush();
        }

        private void StopPrivateChat(string name, string interlocutorName)
        {
            GetConnectedClientByName(interlocutorName).ClosePrivateChatWith(name);
        }

        private void ClosePrivateChatWith(string interlocutorName)
        {
            writer.Write((int)Requests.EndPrivateChat);
            writer.Write(interlocutorName);
            writer.Flush();
        }

        private ConnectedClient GetConnectedClientByName(string name)
        {
            ConnectedClient c = null;
            var allClients = server.Clients.ToList();
            for (int i = 0; i < allClients.Count(); i++)
            {
                if (allClients[i].Name == name)
                    c = allClients[i];
            }
            return c;
        }

        public void StartPrivateChatBetween(string firstInterlocutor, string secondInterlocutor)
        {
            GetConnectedClientByName(firstInterlocutor).OpenPrivateChatForm(secondInterlocutor);
            GetConnectedClientByName(secondInterlocutor).OpenPrivateChatForm(firstInterlocutor);
        }

        private void SignIn(string login, string pass)
        {
            server.Logging("Login new client");
            if (!server.CheckUser(login, pass))
            {
                server.Logging("Try to enter with wrong credentials");
                writer.Write((int)Requests.WrongCredentials);
            }
            else if (CheckIsUserLoggedAlready(login))
            {
                server.Logging("User with this nick is already on server");
                writer.Write((int)Requests.UserAlreadyOnServer);
            }
            else
            {
                server.Logging($"{login} logged in seccesfully");
                Name = login;
                writer.Write((int)Requests.LoginOK);
                server.UpdateUsersList();
            }
            writer.Flush();
        }

        private bool CheckIsUserLoggedAlready(string login)
        {
            bool isAlreadyOnServer = false;
            foreach (var client in server.Clients.ToList())
                if (login == client.Name)
                    isAlreadyOnServer = true;
            return isAlreadyOnServer;
        }

        public void SendUsersList()
        {
            writer.Write((int)Requests.UsersListUpdateStart);
            writer.Flush();
            writer.Write(server.ClientsNames.Count);
            writer.Flush();
            foreach (var name in server.ClientsNames)
            {
                writer.Write(name);
                writer.Flush();
            }
        }

        private void SignUp(string login, string pass)
        {
            server.Logging("Signup new client");
            if (server.CheckUser(login, pass))
            {
                server.Logging("Client was trying to register with existing credentials");
                writer.Write((int)Requests.NickAlreadyTaken);
                writer.Flush();
            }
            else
            {
                server.RegisterNewUser(login, pass);
                SignIn(login, pass);
            }
        }

        private void SendMessage(string from, string message)
        {
            writer.Write((int)Requests.Message);
            writer.Write(from);
            writer.Write(message);
            writer.Flush();
        }

        private void SendPrivateMessage(string from, string message)
        {
            writer.Write((int)Requests.PrivateMessage);
            writer.Write(from);
            writer.Write(message);
            writer.Flush();
        }

    }
}
