// Domain/Contracts/IRemoteControl.cs
namespace Lab1.SmartTvRemote.Domain.Contracts
{
    using System.Collections.Generic;

    public interface IRemoteControl
    {
        string Model { get; }
        void Pair(ISamsungTu7000 tv);
        bool TryExecute(string userInput);              // e.g., "1", "setch 12"
        IEnumerable<IRemoteCommand> VisibleCommands();  // commands for the paired TV
    }
}
