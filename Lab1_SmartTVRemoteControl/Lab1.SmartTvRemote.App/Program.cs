using Lab1.RemoteController.Domain.Contracts;

static void Main()
{
    var screen = new Screen();
    var tvs = new List<ISamsungTu7000>
    {
        new SamsungTu7000_43(screen),
        new SamsungTu7000_55(screen),
        new SamsungTu7000_65(screen)
    };

    ISamsungTu7000 current = tvs[0];
    var remote = new RemoteTM1240A(current);

    while (true)
    {
        Console.Clear();
        AsciiArt.PrintRemote();                         // optional fun
        PrintTvs(tvs, current);
        PrintRemoteMenu();

        var input = Console.ReadLine()?.Trim() ?? string.Empty;
        if (HandleTvSelection(input, tvs, ref current, remote)) continue;
        if (HandleRemoteCommand(input, remote)) continue;

        if (input.Equals("q", StringComparison.OrdinalIgnoreCase)) break;
    }
}
