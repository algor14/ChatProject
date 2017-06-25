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
    public class Client
    {
        private FormsManager manager;
        public string Name { get; set; }
        private TcpClient client;
        private BinaryWriter writer;
        private BinaryReader reader;
        public List<string> ClientsNames = new List<string>();
        private Dictionary<string, PrivateChatForm> privateChatForms = new Dictionary<string, PrivateChatForm>();
        private bool isRunning = true;

        public Client()
        {
            client = new TcpClient();
        }

        public void Start(IPAddress address, FormsManager manager)
        {
            this.manager = manager;
            client.Connect(address, 8080);
            var stream = client.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);

            new Thread(ClientProc).Start();
        }

        public void RegisterLoginUser(Requests req, string nick, string pass)
        {
            writer.Write((int)req);
            writer.Write(nick + ":" + pass);
            writer.Flush();
        }

        private void ClientProc()
        {
            while (isRunning)
            {
                try
                {
                    switch ((Requests)reader.ReadInt32())
                    {
                        case Requests.Message:
                            string fromWho = reader.ReadString();
                            string message = reader.ReadString();
                            MessageReceived?.Invoke(fromWho, message);
                            break;
                        case Requests.PrivateMessage:
                            fromWho = reader.ReadString();
                            message = reader.ReadString();
                            PrivateMessageReceived?.Invoke(fromWho, message);
                            break;
                        case Requests.SystemMessage:
                            message = reader.ReadString();
                            SystemMessageReceived?.Invoke(message);
                            break;
                        case Requests.LoginOK:
                            LoginSuccess?.Invoke();
                            break;
                        case Requests.NickAlreadyTaken:
                            CurrentStatusReceived?.Invoke("This nick is taken");
                            break;
                        case Requests.UserAlreadyOnServer:
                            CurrentStatusReceived?.Invoke("It seems you are already on server");
                            break;
                        case Requests.WrongCredentials:
                            CurrentStatusReceived?.Invoke("Check you nick and password and try again");
                            break;
                        case Requests.UsersListUpdateStart:
                            ReceiveNewUsersList();
                            break;
                        case Requests.StartPrivateChat:
                            string interlocutor = reader.ReadString();
                            OpenPrivateChatForm(interlocutor);
                            break;
                        case Requests.EndPrivateChat:
                            interlocutor = reader.ReadString();
                            ClosePrivateChatForm(interlocutor);
                            break;
                    }
                }
                catch (Exception e)
                {
                    isRunning = false;
                    SystemMessageReceived?.Invoke(e.Message);
                    SystemMessageReceived?.Invoke("Please restart your client");
                }
            }
        }

        private void ClosePrivateChatForm(string interlocutor)
        {
            manager.ClosePrivateChatFormn(privateChatForms[interlocutor]);
            privateChatForms.Remove(interlocutor);
        }

        public void ExitPrivateChat(string interlocutor)
        {
            writer.Write((int)Requests.EndPrivateChat);
            writer.Write(interlocutor);
            writer.Flush();
            privateChatForms.Remove(interlocutor);
        }

        private void OpenPrivateChatForm(string interlocutor)
        {
            privateChatForms[interlocutor] = manager.CreatePrivateChatForm(this, interlocutor);
        }

        public void StartPrivateChatWith(string selectedUser)
        {
            if (selectedUser == Name)
            {
                SystemMessageReceived?.Invoke("You can't start privat chat with yourself");
                return;
            }
            if (privateChatForms.ContainsKey(selectedUser))
            {
                SystemMessageReceived?.Invoke("You already have private chat with this user");
                return;
            }
            writer.Write((int)Requests.StartPrivateChat);
            writer.Write(selectedUser);
            writer.Flush();
        }

        public void ExitMainChat()
        {
            if (isRunning)
            {
                writer.Write((int)Requests.ExitMainChat);
                writer.Flush();
            }
            isRunning = false;
            System.Windows.Forms.Application.Exit();
        }

        private void ReceiveNewUsersList()
        {
            ClientsNames = new List<string>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                ClientsNames.Add(reader.ReadString());
            }
            UsersListUpdate?.Invoke();
        }

        public void SendMessage(string message, string interlocutor = null)
        {
            if (interlocutor != null)
            {
                writer.Write((int)Requests.PrivateMessage);
                writer.Write(interlocutor);
            }
            else
            {
                writer.Write((int)Requests.Message);
            }
            writer.Write(message);
            writer.Flush();
        }

        public event Action<string> SystemMessageReceived;
        public event Action<string, string> MessageReceived;
        public event Action<string, string> PrivateMessageReceived;
        public event Action<string> CurrentStatusReceived;
        public event Action LoginSuccess;
        public event Action UsersListUpdate;
    }
}
