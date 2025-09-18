using Lab1.SmartTvRemote.Domain.Contracts;
using Lab1.SmartTvRemote.Domain.UI;

public static class ConsoleShell
{
    public static void Run(IRemoteControl remote, List<ISamsungTu7000> tvs, Screen screen, ISamsungTu7000 current)
    {
        ConsoleRenderer.Render(remote, tvs, current);

        while (true)
        {
            Console.Write("> ");
            var input = (Console.ReadLine() ?? string.Empty).Trim();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
                break;

            // switch active TV
            if (ConsoleRenderer.TrySwitchTv(input, tvs, ref current, remote))
            {
                ConsoleRenderer.Render(remote, tvs, current);
                continue;
            }

            // execute remote command first
            if (remote.TryExecute(input))
            {
                // open subm menus as needed
                if (string.Equals(input, "0", StringComparison.OrdinalIgnoreCase))
                {
                    if (!MenuController.EnsureTvOn(current, screen, "Source")) { continue; }
                    MenuController.SourceMenuLoop(remote, current, tvs, screen);
                    ConsoleRenderer.Render(remote, tvs, current, null);
                    continue;
                }
                else if (string.Equals(input, "s", StringComparison.OrdinalIgnoreCase))
                {
                    if (!MenuController.EnsureTvOn(current, screen, "Smart")) { continue; }
                    var msg = MenuController.SmartMenuLoop(remote, current, screen);
                    ConsoleRenderer.Render(remote, tvs, current, msg);
                    continue;
                }
                else if (string.Equals(input, "t", StringComparison.OrdinalIgnoreCase))
                {
                    if (!MenuController.EnsureTvOn(current, screen, "Settings")) { continue; }
                    MenuController.SettingsMenuLoop(remote, current, tvs, screen);
                    ConsoleRenderer.Render(remote, tvs, current, null);
                    continue;
                }

                // back to dashboard
                ConsoleRenderer.Render(remote, tvs, current, $"Executed: {input}");
            }
            else
            {
                ConsoleRenderer.Render(remote, tvs, current, "Unknown command.");
            }
        }
    }
}
