using System;
using System.Collections.Generic;

namespace ChatMediatorPattern
{
    public interface IMediator
    {
        void SendMessage(string message, IUser user);
        void AddUser(IUser user);
    }

    public class ChatMediator : IMediator
    {
        private readonly List<IUser> users = new List<IUser>();

        public void SendMessage(string message, IUser user)
        {
            foreach (var u in users)
            {
                if (u != user)
                {
                    u.Receive(message);
                }
            }
        }

        public void AddUser(IUser user) => users.Add(user);
    }

    public interface IUser
    {
        void Send(string message);
        void Receive(string message);
    }

    public class User : IUser
    {
        private readonly string name;
        private readonly IMediator mediator;

        public User(string name, IMediator mediator)
        {
            this.name = name;
            this.mediator = mediator;
        }

        public void Send(string message)
        {
            Console.WriteLine($"{name} отправил: {message}");
            mediator.SendMessage(message, this);
        }

        public void Receive(string message) => Console.WriteLine($"{name} получил: {message}");
    }

    public class Program
    {
        public static void Main()
        {
            var mediator = new ChatMediator();

            var user1 = new User("Пользователь 1", mediator);
            var user2 = new User("Пользователь 2", mediator);
            var user3 = new User("Пользователь 3", mediator);

            mediator.AddUser(user1);
            mediator.AddUser(user2);
            mediator.AddUser(user3);

            user1.Send("Привет всем!");
            user2.Send("Привет, Пользователь 1!");
            user3.Send("Здравствуйте, участники чата!");
        }
    }
}
