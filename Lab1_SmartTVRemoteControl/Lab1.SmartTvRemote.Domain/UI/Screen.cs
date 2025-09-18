using Lab1.SmartTvRemote.Domain.Contracts;

namespace Lab1.SmartTvRemote.Domain.UI
{
    // simulates the TV screen rendering various states and menus
    public class Screen
    {
        public void RenderStartup(string model)
        {
            Console.WriteLine($"[Screen] Booting {model}...");
        }

        public void RenderPower(bool on)
        {
            Console.WriteLine(on ? "[Screen] Power: ON" : "[Screen] Power: OFF");
        }

        public void RenderVolume(int vol, bool muted)
        {
            Console.WriteLine(muted ? $"[Screen] Volume: MUTED" : $"[Screen] Volume: {vol}");
        }

        public void RenderChannel(int ch)
        {
            Console.WriteLine($"[Screen] Channel: {ch}");
        }

        public void RenderSmartMenu()
        {
            Console.WriteLine();
            Console.WriteLine("+---------------- Smart Menu ----------------+");
            Console.WriteLine("| [N] Netflix   | [H] Hulu   | [A] Prime     |");
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("| [R] Return to TV           | [X] Exit      |");
            Console.WriteLine("+--------------------------------------------+");
            Console.WriteLine();
        }

        public void RenderSourcePanel(ISamsungTu7000 tv)
        {
            Console.WriteLine();
            Console.WriteLine("+---------------- Source ---------------------------+");
            Console.WriteLine($"| Current: {tv.Input,-40}|");
            Console.WriteLine("| Cycle with [0]: TV -> HDMI1 -> HDMI2 -> AV -> Apps|");
            Console.WriteLine("|---------------------------------------------------|");
            Console.WriteLine("| [R] Return to TV           | [X] Exit             |");
            Console.WriteLine("+---------------------------------------------------+");
            Console.WriteLine();
        }

        public void RenderSettingsMenu(int brightness, int contrast)
        {
            Console.WriteLine();
            Console.WriteLine("+---------------- Settings ------------------------+");
            Console.WriteLine($"| Brightness: {brightness,3}  (use: 'b +', 'b -') |");
            Console.WriteLine($"| Contrast  : {contrast,3}  (use: 'c +', 'c -')   |");
            Console.WriteLine("|--------------------------------------------------|");
            Console.WriteLine("| [R] Return to TV           | [X] Exit            |");
            Console.WriteLine("+--------------------------------------------------+");
            Console.WriteLine();
        }

        public void RenderSettingsChanged(string what, int value)
        {
            Console.WriteLine($"[Screen] {what} set to {value}");
        }

        public void RenderSummary(string summary)
        {
            Console.WriteLine($"[Screen] {summary}");
        }
    }
}
