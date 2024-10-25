using System;
using System.Collections.Generic;

namespace SmartHomeCommandPattern
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class Light
    {
        public void On() => Console.WriteLine("Свет включен");
        public void Off() => Console.WriteLine("Свет выключен");
    }

    public class AC
    {
        public void On() => Console.WriteLine("Кондиционер включен");
        public void Off() => Console.WriteLine("Кондиционер выключен");
    }

    public class TV
    {
        public void On() => Console.WriteLine("Телевизор включен");
        public void Off() => Console.WriteLine("Телевизор выключен");
    }

    public class LightOnCommand : ICommand
    {
        private readonly Light light;
        public LightOnCommand(Light light) => this.light = light;
        public void Execute() => light.On();
        public void Undo() => light.Off();
    }

    public class LightOffCommand : ICommand
    {
        private readonly Light light;
        public LightOffCommand(Light light) => this.light = light;
        public void Execute() => light.Off();
        public void Undo() => light.On();
    }

    public class ACOnCommand : ICommand
    {
        private readonly AC ac;
        public ACOnCommand(AC ac) => this.ac = ac;
        public void Execute() => ac.On();
        public void Undo() => ac.Off();
    }

    public class ACOffCommand : ICommand
    {
        private readonly AC ac;
        public ACOffCommand(AC ac) => this.ac = ac;
        public void Execute() => ac.Off();
        public void Undo() => ac.On();
    }

    public class TVOnCommand : ICommand
    {
        private readonly TV tv;
        public TVOnCommand(TV tv) => this.tv = tv;
        public void Execute() => tv.On();
        public void Undo() => tv.Off();
    }

    public class TVOffCommand : ICommand
    {
        private readonly TV tv;
        public TVOffCommand(TV tv) => this.tv = tv;
        public void Execute() => tv.Off();
        public void Undo() => tv.On();
    }

    public class RemoteControl
    {
        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
        private readonly Stack<ICommand> commandHistory = new Stack<ICommand>();

        public void SetCommand(string slot, ICommand command) => commands[slot] = command;

        public void PressButton(string slot)
        {
            if (commands.TryGetValue(slot, out var command))
            {
                command.Execute();
                commandHistory.Push(command);
            }
            else
            {
                Console.WriteLine("Команда не назначена на этот слот.");
            }
        }

        public void UndoButton()
        {
            if (commandHistory.Count > 0)
            {
                commandHistory.Pop().Undo();
            }
            else
            {
                Console.WriteLine("Нет команд для отмены.");
            }
        }
    }

    public class MacroCommand : ICommand
    {
        private readonly List<ICommand> commands = new List<ICommand>();

        public void AddCommand(ICommand command) => commands.Add(command);

        public void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            for (var i = commands.Count - 1; i >= 0; i--)
            {
                commands[i].Undo();
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            var remote = new RemoteControl();
            var light = new Light();
            var ac = new AC();
            var tv = new TV();

            var lightOn = new LightOnCommand(light);
            var lightOff = new LightOffCommand(light);
            var acOn = new ACOnCommand(ac);
            var acOff = new ACOffCommand(ac);
            var tvOn = new TVOnCommand(tv);
            var tvOff = new TVOffCommand(tv);

            remote.SetCommand("LightOn", lightOn);
            remote.SetCommand("LightOff", lightOff);
            remote.SetCommand("ACOn", acOn);
            remote.SetCommand("ACOff", acOff);
            remote.SetCommand("TVOn", tvOn);
            remote.SetCommand("TVOff", tvOff);

            remote.PressButton("LightOn");
            remote.PressButton("ACOn");
            remote.PressButton("TVOn");

            remote.UndoButton();
            remote.UndoButton();
            remote.UndoButton();

            var macroCommand = new MacroCommand();
            macroCommand.AddCommand(lightOn);
            macroCommand.AddCommand(acOn);
            remote.SetCommand("Macro", macroCommand);
            remote.PressButton("Macro");
        }
    }
}
