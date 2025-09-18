// Domain/Contracts/ISamsungTu7000.cs
namespace Lab1.SmartTvRemote.Domain.Contracts
{
    using Lab1.SmartTvRemote.Domain.Enums;
    public interface ISamsungTu7000
    {
        string Model { get; }
        int SizeInches { get; }
        TvFeatures SupportedFeatures { get; }

        bool IsOn { get; }
        bool IsMuted { get; }
        bool IsSmartMode { get; }
        int Volume { get; }
        int MaxVolume { get; }
        int Channel { get; }
        string GetStateSummary();
        string? ActiveApp { get; }
        InputSource Input { get; }
        void PowerToggle();
        void VolumeUp();
        void VolumeDown();
        void SetVolume(int value);
        void ChannelUp();
        void ChannelDown();
        void SetChannel(int channelNumber);
        void MuteToggle();
        void OpenSmartMenu();
        void OpenSettings();
        void OpenSource();
        void OpenApp(string appName);
        void AdjustSetting(string which, string[] args);
        void Return();
        void Exit();

        // events to notify about state changes
        event EventHandler? PowerStateChanged;
        event EventHandler<int>? VolumeChanged;
        event EventHandler<int>? ChannelChanged;
        event EventHandler<bool>? MuteChanged;
        event EventHandler<InputSource>? InputChanged;
    }
}
