using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProject
{
    public class FormsManager
    {
        private LoginForm loginForm;
        private ChatForm chatForm;

        public void CreateLoginForm(Client client, MainForm mainForm)
        {
            loginForm = new LoginForm(client, this);
            new Thread(ShowLoginForm).Start();
            mainForm.Close();
        }

        private void ShowLoginForm()
        {
            loginForm.ShowDialog();
        }

        public void CreateChatForm(Client client)
        {
            chatForm = new ChatForm(client);
            new Thread(ShowChatForm).Start();
        }

        private void ShowChatForm()
        {
            chatForm.ShowDialog();
        }

        public void CloseLoginForm()
        {
            loginForm.Invoke((MethodInvoker)delegate
            {
                loginForm.Close();
            });
        }

        public PrivateChatForm CreatePrivateChatForm(Client client, string interlocutor)
        {
            var privateChatForm = new PrivateChatForm(client, interlocutor);
            new Thread(() => { ShowPrivateChatForm(privateChatForm);}).Start();
            return privateChatForm;
        }

        private void ShowPrivateChatForm(PrivateChatForm privateChatForm)
        {
            privateChatForm.ShowDialog();
        }

        public void ClosePrivateChatFormn(PrivateChatForm privateChatForm)
        {
            privateChatForm.Invoke((MethodInvoker)delegate
            {
                privateChatForm.Close();
            });
        }
    }
}
