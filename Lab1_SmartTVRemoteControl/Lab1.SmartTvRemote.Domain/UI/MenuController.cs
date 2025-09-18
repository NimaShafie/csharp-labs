using Lab1.SmartTvRemote.Domain.Contracts;
using Lab1.SmartTvRemote.Domain.UI;

public static class MenuController
{
    public static bool EnsureTvOn(ISamsungTu7000 tv, Screen screen, string menuName)
    {
        if (!tv.IsOn)
        {
            screen.RenderSummary($"{menuName} menu unavailable (TV is OFF)");
            return false;
        }
        return true;
    }

    public static string? SmartMenuLoop(IRemoteControl remote, ISamsungTu7000 tv, Screen screen)
    {
        while (true)
        {
            Console.Write("Smart> ");
            var cmd = (Console.ReadLine() ?? "").Trim();

            if (string.Equals(cmd, "r", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("R"); return "[Screen] Return"; }
            if (string.Equals(cmd, "x", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("X"); return "[Screen] Exit"; }

            if (string.Equals(cmd, "n", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("N"); return "[Screen] Launching app: Netflix"; }
            if (string.Equals(cmd, "h", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("H"); return "[Screen] Launching app: Hulu"; }
            if (string.Equals(cmd, "a", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("A"); return "[Screen] Launching app: Prime Video"; }

            Console.WriteLine("Use [N] Netflix, [H] Hulu, [A] Prime Video, [R] Return, [X] Exit.");
        }
    }

    public static void SourceMenuLoop(IRemoteControl remote, ISamsungTu7000 tv, IReadOnlyList<ISamsungTu7000> tvs, Screen screen)
    {
        screen.RenderSourcePanel(tv);

        while (true)
        {
            Console.Write("Source> ");
            var cmd = (Console.ReadLine() ?? "").Trim();

            if (string.Equals(cmd, "r", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("R"); break; }
            if (string.Equals(cmd, "x", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("X"); break; }

            if (string.Equals(cmd, "0", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(cmd, "o", StringComparison.OrdinalIgnoreCase))
            {
                remote.TryExecute("0");

                ConsoleRenderer.Render(remote, tvs, tv, null);
                screen.RenderSourcePanel(tv);
                continue;
            }

            Console.WriteLine("Use [0] to cycle: TV -> HDMI1 -> HDMI2 -> Apps -> TV, [R] Return, [X] Exit.");
        }
    }

    public static void SettingsMenuLoop(IRemoteControl remote, ISamsungTu7000 tv, IReadOnlyList<ISamsungTu7000> tvs, Screen screen)
    {
        while (true)
        {
            Console.Write("Settings> ");
            var raw = (Console.ReadLine() ?? "").Trim();

            if (string.Equals(raw, "r", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("R"); break; }
            if (string.Equals(raw, "x", StringComparison.OrdinalIgnoreCase)) { remote.TryExecute("X"); break; }

            var parts = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var isBrightness = parts.Length == 2 && parts[0].Equals("b", StringComparison.OrdinalIgnoreCase);
            var isContrast = parts.Length == 2 && parts[0].Equals("c", StringComparison.OrdinalIgnoreCase);
            var isPlusMinus = parts.Length == 2 && (parts[1] == "+" || parts[1] == "-");

            if ((isBrightness || isContrast) && isPlusMinus)
            {
                remote.TryExecute($"{parts[0]} {parts[1]}");

                ConsoleRenderer.Render(remote, tvs, tv, null);
                remote.TryExecute("T");
                continue;
            }

            Console.WriteLine("Use 'b +'/'b -' (brightness), 'c +'/'c -' (contrast). [R]=Return, [X]=Exit.");
        }
    }
}
