// Domain/Models/SamsungTu7000.cs
namespace Lab1.SmartTvRemote.Domain.Models
{
    using Lab1.SmartTvRemote.Domain.Contracts;
    using Lab1.SmartTvRemote.Domain.Enums;
    using Lab1.SmartTvRemote.Domain.UI;

    public sealed class SamsungTu7000 : ISamsungTu7000
    {
        private const int DEFAULT_MAX_VOLUME = 10;

        private readonly Screen _screen;

        private bool _isOn;
        private bool _isMuted;
        private int _volume;
        private int _channel;
        private InputSource _input = InputSource.TV;
        private string? _activeApp;
        private int _brightness = 50;
        private int _contrast = 50;

        private SamsungTu7000(Screen screen, string modelCode, int sizeInches)
        {
            _screen = screen ?? throw new ArgumentNullException(nameof(screen));
            Model = modelCode;
            SizeInches = sizeInches;

            _volume = 3;
            _channel = 2;
        }

        public string Model { get; }
        public int SizeInches { get; }
        public TvFeatures SupportedFeatures => TvFeatures.HDR | TvFeatures.SmartMenu | TvFeatures.GameMode;

        public bool IsOn => _isOn;
        public bool IsMuted => _isMuted;
        public int Volume => _volume;
        public int Channel => _channel;
        public int MaxVolume => DEFAULT_MAX_VOLUME;
        public InputSource Input => _input;

        public bool IsSmartMode => _input == InputSource.Apps;
        public string? ActiveApp => _activeApp;

        public event EventHandler? PowerStateChanged;
        public event EventHandler<int>? VolumeChanged;
        public event EventHandler<int>? ChannelChanged;
        public event EventHandler<bool>? MuteChanged;
        public event EventHandler<InputSource>? InputChanged;

        public void PowerToggle()
        {
            bool wasOn = _isOn;
            _isOn = !_isOn;
            PowerStateChanged?.Invoke(this, EventArgs.Empty);
            if (!wasOn && _isOn) _screen.RenderStartup(Model);
            _screen.RenderPower(_isOn);
        }

        public void VolumeUp()
        {
            if (!_isOn) return;
            _isMuted = false;
            _volume = Math.Min(_volume + 1, MaxVolume);
            VolumeChanged?.Invoke(this, _volume);
            MuteChanged?.Invoke(this, _isMuted);
            _screen.RenderVolume(_volume, _isMuted);
        }

        public void VolumeDown()
        {
            if (!_isOn) return;
            _isMuted = false;
            _volume = Math.Max(_volume - 1, 0);
            VolumeChanged?.Invoke(this, _volume);
            MuteChanged?.Invoke(this, _isMuted);
            _screen.RenderVolume(_volume, _isMuted);
        }

        public void SetVolume(int value)
        {
            if (!_isOn) return;
            _isMuted = false;
            _volume = Math.Clamp(value, 0, MaxVolume);
            VolumeChanged?.Invoke(this, _volume);
            MuteChanged?.Invoke(this, _isMuted);
            _screen.RenderVolume(_volume, _isMuted);
        }

        public void ChannelUp()
        {
            if (!_isOn) return;
            _channel = Math.Max(1, _channel + 1);
            if (_input != InputSource.TV) { _input = InputSource.TV; _activeApp = null; InputChanged?.Invoke(this, _input); }
            ChannelChanged?.Invoke(this, _channel);
            _screen.RenderChannel(_channel);
        }

        public void ChannelDown()
        {
            if (!_isOn) return;
            _channel = Math.Max(1, _channel - 1);
            if (_input != InputSource.TV) { _input = InputSource.TV; _activeApp = null; InputChanged?.Invoke(this, _input); }
            ChannelChanged?.Invoke(this, _channel);
            _screen.RenderChannel(_channel);
        }

        public void SetChannel(int channelNumber)
        {
            if (!_isOn) return;
            channelNumber = Math.Clamp(channelNumber, 1, 1000);
            _channel = channelNumber;
            if (_input != InputSource.TV) { _input = InputSource.TV; _activeApp = null; InputChanged?.Invoke(this, _input); }
            ChannelChanged?.Invoke(this, _channel);
            _screen.RenderChannel(_channel);
        }

        public void MuteToggle()
        {
            if (!_isOn) return;
            _isMuted = !_isMuted;
            MuteChanged?.Invoke(this, _isMuted);
            _screen.RenderVolume(_volume, _isMuted);
        }

        public void OpenSmartMenu()
        {
            if (!_isOn) return;
            if (!SupportedFeatures.HasFlag(TvFeatures.SmartMenu)) return;
            _screen.RenderSmartMenu();
        }

        public void OpenSettings()
        {
            if (!_isOn) return;
            _screen.RenderSettingsMenu(_brightness, _contrast);
        }

        public void OpenSource()
        {
            if (!IsOn) return;
            var prev = _input;

            _input = _input switch
            {
                InputSource.TV => InputSource.HDMI1,
                InputSource.HDMI1 => InputSource.HDMI2,
                InputSource.HDMI2 => InputSource.AV,
                InputSource.AV => InputSource.Apps,
                InputSource.Apps => InputSource.TV,
                _ => InputSource.TV
            };

            if (prev == InputSource.Apps && _input != InputSource.Apps) _activeApp = null;
            InputChanged?.Invoke(this, _input);
            _screen.RenderSummary($"Source: {_input}");
        }

        public void AdjustSetting(string which, string[] args)
        {
            if (!IsOn) return;
            if (args.Length == 0) return;

            int delta = args[0] == "+" ? +5 : args[0] == "-" ? -5 : 0;
            if (delta == 0) return;

            switch (which.ToLowerInvariant())
            {
                case "brightness":
                    _brightness = Math.Clamp(_brightness + delta, 0, 100);
                    _screen.RenderSettingsChanged("Brightness", _brightness);
                    break;
                case "contrast":
                    _contrast = Math.Clamp(_contrast + delta, 0, 100);
                    _screen.RenderSettingsChanged("Contrast", _contrast);
                    break;
            }
        }

        public void OpenApp(string appName)
        {
            if (!IsOn) return;
            _input = InputSource.Apps;
            _activeApp = appName;
            InputChanged?.Invoke(this, _input);
            _screen.RenderSummary($"Launching app: {appName}");
        }

        public void Return()
        {
            if (!IsOn) return;
            if (_input == InputSource.Apps) { _activeApp = null; _input = InputSource.TV; InputChanged?.Invoke(this, _input); }
            _screen.RenderSummary("Return");
        }

        public void Exit()
        {
            if (!IsOn) return;
            _screen.RenderSummary("Exit");
        }

        public string GetStateSummary() =>
            $"{Model} {SizeInches:0}\" | " +
            (_isOn ? $"Ch {_channel} | Vol {(_isMuted ? "MUTED" : _volume.ToString())} | {Input}" : "OFF");

        public static SamsungTu7000 Create(Screen screen, SamsungTu7000Size size)
        {
            var code = SamsungTu7000Catalog.GetCodeOrThrow(size);
            return new SamsungTu7000(screen, code, (int)size);
        }

        public static bool TryCreate(Screen screen, SamsungTu7000Size size, out SamsungTu7000? tv)
        {
            if (!SamsungTu7000Catalog.TryGetCode(size, out var code))
            {
                tv = null;
                return false;
            }
            tv = new SamsungTu7000(screen, code, (int)size);
            return true;
        }
    }
}
