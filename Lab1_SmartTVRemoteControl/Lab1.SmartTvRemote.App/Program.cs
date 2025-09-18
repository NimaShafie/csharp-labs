using Lab1.SmartTvRemote.Domain.Contracts;
using Lab1.SmartTvRemote.Domain.Models;
using Lab1.SmartTvRemote.Domain.Remote;
using Lab1.SmartTvRemote.Domain.UI;

class Program
{
    static void Main()
    {
        var screen = new Screen();

        // TVs (all start OFF)
        var tvs = new List<ISamsungTu7000>
        {
            SamsungTu7000.Create(screen, SamsungTu7000Size.In43),
            SamsungTu7000.Create(screen, SamsungTu7000Size.In50),
            SamsungTu7000.Create(screen, SamsungTu7000Size.In55),
            SamsungTu7000.Create(screen, SamsungTu7000Size.In58),
            SamsungTu7000.Create(screen, SamsungTu7000Size.In65),
            SamsungTu7000.Create(screen, SamsungTu7000Size.In70),
            SamsungTu7000.Create(screen, SamsungTu7000Size.In75),
        };

        var current = tvs[0];
        var remote = new RemoteTM1240A(current);

        foreach (var tv in tvs)
        {
            tv.PowerStateChanged += (_, __) => screen.RenderSummary(tv.GetStateSummary());
            tv.VolumeChanged += (_, __) => screen.RenderSummary(tv.GetStateSummary());
            tv.ChannelChanged += (_, __) => screen.RenderSummary(tv.GetStateSummary());
            tv.MuteChanged += (_, __) => screen.RenderSummary(tv.GetStateSummary());
            tv.InputChanged += (_, __) => screen.RenderSummary(tv.GetStateSummary());
        }

        ConsoleShell.Run(remote, tvs, screen, current);
    }
}
