using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject
{
    public enum Requests
    {
        ConnectionOK,
        Message,
        PrivateMessage,
        SystemMessage,
        Signup,
        Signin,
        NickAlreadyTaken,
        WrongCredentials,
        UserAlreadyOnServer,
        LoginOK,
        UsersListUpdateStart,
        ExitMainChat,
        StartPrivateChat,
        EndPrivateChat
    }
}
