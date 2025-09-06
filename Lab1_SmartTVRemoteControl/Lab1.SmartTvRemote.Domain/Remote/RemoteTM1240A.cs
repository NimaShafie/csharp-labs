using Lab1.RemoteController.Domain.Contracts;

public class RemoteTM1240A /* : IRemoteControl (optional) */
{
    private ISamsungTu7000 _target;

    public RemoteTM1240A(ISamsungTu7000 initialTarget) => _target = initialTarget;

    public void Pair(ISamsungTu7000 tv) => _target = tv;

    // Button methods (map 1:1 to TV commands)
    public void Power() => _target.PowerToggle();
    public void VolUp() => _target.VolumeUp();
    public void VolDown() => _target.VolumeDown();
    public void Mute() => _target.MuteToggle();
    public void ChanUp() => _target.ChannelUp();
    public void ChanDown() => _target.ChannelDown();
    public void SetChannel(int ch) => _target.SetChannel(ch);
    public void Smart() => _target.OpenSmartMenu();
    public void Settings() => _target.OpenSettings();

    // (Optional) expose events: ButtonPressed(string buttonName)
}
