// Domain/Remote/RemoteTM1240A.cs
namespace Lab1.SmartTvRemote.Domain.Remote
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Lab1.SmartTvRemote.Domain.Contracts;

    public sealed class RemoteTM1240A : IRemoteControl
    {
        private ISamsungTu7000 _target;
        private readonly Dictionary<string, IRemoteCommand> _commands;

        public RemoteTM1240A(ISamsungTu7000 initialTarget)
        {
            _target = initialTarget ?? throw new ArgumentNullException(nameof(initialTarget));
            _commands = BuiltInCommands.All().ToDictionary(c => c.Key, StringComparer.OrdinalIgnoreCase);
        }

        public string Model => "TM-1240A";

        public void Pair(ISamsungTu7000 tv) => _target = tv ?? throw new ArgumentNullException(nameof(tv));

        public IEnumerable<IRemoteCommand> VisibleCommands() =>
            _commands.Values.Where(c => c.IsVisibleFor(_target)).OrderBy(c => c.Key);

        // returns true if the command was found and executed
        public bool TryExecute(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput)) return false;

            var parts = userInput.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var key = parts[0];
            var args = parts.Skip(1).ToArray();

            if (_commands.TryGetValue(key, out var cmd) && cmd.IsVisibleFor(_target))
            {
                cmd.Execute(_target, args);
                return true;
            }
            return false;
        }
    }
}
