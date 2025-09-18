// Domain/Contracts/IRemoteCommand.cs
namespace Lab1.SmartTvRemote.Domain.Contracts
{
    public interface IRemoteCommand
    {
        string Name { get; }          // internal name (e.g., "SetChannel", "VolumeUp")
        string Key { get; }           // user input token (e.g., "P", "1", "setch")
        string Description { get; }   // for help/menu

        void Execute(ISamsungTu7000 tv, string[] args);
        bool IsVisibleFor(ISamsungTu7000 tv); // feature-gated visibility
    }
}
