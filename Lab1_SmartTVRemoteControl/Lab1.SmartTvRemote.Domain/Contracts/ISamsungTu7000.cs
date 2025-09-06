namespace Lab1.RemoteController.Domain.Contracts
{
    public interface ISamsungTu7000
    {
        string Brand { get; }
        string Model { get; }
        int SizeInches { get; }
        TvFeatures SupportedFeatures { get; }

        bool IsOn { get; }
        bool IsMuted { get; }
        int Volume { get; }                 // 0..MaxVolume
        int MaxVolume { get; }              // override per model if desired
        int Channel { get; }                // >= 1 (or a channel map if you want)
        InputSource Input { get; }          // TV, HDMI1, HDMI2, etc.

        // Commands
        void PowerToggle();
        void VolumeUp();
        void VolumeDown();
        void SetVolume(int value);
        void ChannelUp();
        void ChannelDown();
        void SetChannel(int channelNumber);
        void MuteToggle();
        void OpenSmartMenu();               // no-op if not supported
        void OpenSettings();

        // Events for the Screen (and tests)
        event EventHandler? PowerStateChanged;
        event EventHandler<int>? VolumeChanged;
        event EventHandler<int>? ChannelChanged;
        event EventHandler<bool>? MuteChanged;
        event EventHandler<InputSource>? InputChanged;
    }
}
