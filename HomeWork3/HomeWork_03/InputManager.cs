using System;
using System.Collections.Generic;
using HomeWork_03.Commands;

namespace HomeWork_03
{
    public class InputManager
    {
        private Dictionary<ConsoleKey, Command> CommandsByKeys { get; } = new Dictionary<ConsoleKey, Command>();

        public void SetOrUpdateCommand(ConsoleKey key, Command command)
        {
            if (CommandsByKeys.ContainsKey(key))
            {
                CommandsByKeys[key] = command;

                return;
            }

            CommandsByKeys.Add(key, command);
        }

        public Command WaitUser()
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (!CommandsByKeys.TryGetValue(key, out var command)) continue;

                return command;
            }
        }
    }
}