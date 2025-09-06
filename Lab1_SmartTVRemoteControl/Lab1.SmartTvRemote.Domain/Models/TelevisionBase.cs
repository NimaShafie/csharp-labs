using Lab1.RemoteController.Domain.Contracts;
using Lab1.SmartTvRemote.Domain.Enums;
using Lab1.SmartTvRemote.Domain.UI;

public abstract class TelevisionBase : ISamsungTu7000
{
    protected readonly Screen Screen;               // composition
    protected int _volume;
    protected int _channel;
    protected bool _isOn;
    protected bool _isMuted;
    protected InputSource _input = InputSource.TV;

    protected TelevisionBase(Screen screen)
    {
        Screen = screen ?? throw new ArgumentNullException(nameof(screen));
        _volume = 10;       // sensible default
        _channel = 2;
    }

    // Metadata — override in derived classes
    public abstract string Brand { get; }
    public abstract string Model { get; }
    public abstract int SizeInches { get; }
    public abstract TvFeatures SupportedFeatures { get; }
    public virtual int MaxVolume => 100;

    // State (encapsulation via properties)
    public bool IsOn => _isOn;
    public bool IsMuted => _isMuted;
    public int Volume => _volume;
    public int Channel => _channel;
    public InputSource Input => _input;

    // Events
    public event EventHandler? PowerStateChanged;
    public event EventHandler<int>? VolumeChanged;
    public event EventHandler<int>? ChannelChanged;
    public event EventHandler<bool>? MuteChanged;
    public event EventHandler<InputSource>? InputChanged;

    // Core behaviors (protected helpers + ITelevision impls)
    public virtual void PowerToggle() { /* update _isOn; raise event; tell Screen */ }
    public virtual void VolumeUp() { /* clamp to MaxVolume; raise event */ }
    public virtual void VolumeDown() { /* ... */ }
    public virtual void SetVolume(int value) { /* ... */ }
    public virtual void ChannelUp() { /* wrap or clamp; raise event */ }
    public virtual void ChannelDown() { /* ... */ }
    public virtual void SetChannel(int channelNumber) { /* validate; raise event */ }
    public virtual void MuteToggle() { /* toggle; raise event */ }
    public virtual void OpenSmartMenu()
    {
        if (!SupportedFeatures.HasFlag(TvFeatures.SmartMenu)) return; // no-op
        // Raise some UI hook or call Screen.RenderSmartMenu(...)
    }
    public virtual void OpenSettings() { /* Screen.RenderSettings(); */ }

    // Optionally expose a method that Screen calls back for a “render snapshot”
    public virtual string GetStateSummary() => $"{Brand} {Model} {SizeInches}\" | " +
        (IsOn ? $"Ch {Channel} | Vol {(IsMuted ? "MUTED" : Volume)} | {Input}" : "OFF");
}
