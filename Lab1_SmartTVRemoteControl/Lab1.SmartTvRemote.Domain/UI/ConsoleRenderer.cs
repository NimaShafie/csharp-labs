using Lab1.SmartTvRemote.Domain.Contracts;
using Lab1.SmartTvRemote.Domain.UI;

public static class ConsoleRenderer
{
    // column widths
    private const int ColModel = 11;
    private const int ColStatus = 6;
    private const int ColMuted = 5;
    private const int ColVol = 7;
    private const int ColMode = 20;
    private const int ColChanApp = 18;

    public static void Render(IRemoteControl remote, IReadOnlyList<ISamsungTu7000> tvs, ISamsungTu7000 current, string? message = null)
    {
        Console.Clear();
        AsciiArt.PrintRemoteWithButtons(remote.VisibleCommands());

        RenderTvTable(tvs, current);

        Console.WriteLine("Type a command (e.g., '1', 'setch 12'), 'tv <#>' to switch TV, or 'q' to quit.");
        if (!string.IsNullOrWhiteSpace(message))
            Console.WriteLine(message);
    }

    public static bool TrySwitchTv(string input, IList<ISamsungTu7000> tvs, ref ISamsungTu7000 current, IRemoteControl remote)
    {
        if (!input.StartsWith("tv ", StringComparison.OrdinalIgnoreCase)) return false;
        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2 && int.TryParse(parts[1], out var idx))
        {
            if (idx >= 1 && idx <= tvs.Count)
            {
                current = tvs[idx - 1];
                remote.Pair(current);
                Console.WriteLine($"Active TV switched to: {current.Model} {current.SizeInches:0}\"");
                return true;
            }
        }
        Console.WriteLine("Invalid TV selection.");
        return false;
    }

    private static void RenderTvTable(IReadOnlyList<ISamsungTu7000> tvs, ISamsungTu7000 current)
    {
        Console.WriteLine();

        string headerRow = BuildHeaderRow();
        string border = new string('-', headerRow.Length);

        var title = CenterLeftWithRightNote("TVs", "(* = active)", headerRow.Length);
        Console.WriteLine(title);
        Console.WriteLine(border);
        Console.WriteLine(headerRow);
        Console.WriteLine(border);

        for (int i = 0; i < tvs.Count; i++)
        {
            var tv = tvs[i];
            bool isActive = ReferenceEquals(tv, current);

            string status = tv.IsOn ? "ON" : "OFF";
            string muted = tv.IsMuted ? "Yes" : "No";
            string vol = $"{tv.Volume}/{tv.MaxVolume}";
            string mode = tv.IsSmartMode
                ? $"Smart ({tv.ActiveApp ?? "-"})"
                : tv.Input.ToString(); // TV, HDMI1, HDMI2, Apps
            string val = tv.IsSmartMode ? (tv.ActiveApp ?? "-") : tv.Channel.ToString();

            Console.WriteLine(
                $"{(isActive ? "*" : " "),1} {(i + 1),1} | " +
                $"{tv.Model,ColModel} | {status,ColStatus} | {muted,ColMuted} | {vol,ColVol} | " +
                $"{mode,ColMode} | {val,ColChanApp} |"
            );
        }

        Console.WriteLine(border);
        Console.WriteLine("Switch active TV: 'tv 1' or 'tv 2' ...");
        Console.WriteLine();
    }

    private static string BuildHeaderRow()
    {
        return
            $"#   | {"Model",ColModel} | {"Status",ColStatus} | {"Muted",ColMuted} | " +
            $"{"Volume",ColVol} | {"Mode",ColMode} | {"Channel/App",ColChanApp} |";
    }

    private static string CenterLeftWithRightNote(string leftTitle, string rightNote, int width)
    {
        const int gap = 2;
        int leftWidth = Math.Max(0, width - rightNote.Length - gap);
        string centeredLeft = leftTitle.PadLeft((leftWidth + leftTitle.Length) / 2).PadRight(leftWidth);
        return centeredLeft + new string(' ', gap) + rightNote;
    }
}
