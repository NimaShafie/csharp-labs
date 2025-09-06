public class Screen
{
    // This is intentionally dumb: it only renders state it’s told about.
    // The TV pushes updates via events; Screen decides how to show them (Console).
    public void RenderStartup(string model) { /* Console.WriteLine splash */ }
    public void RenderPower(bool on) { /* ... */ }
    public void RenderVolume(int vol, bool muted) { /* ... */ }
    public void RenderChannel(int ch) { /* ... */ }
    public void RenderSmartMenu() { /* ASCII menu */ }
    public void RenderSettings() { /* ASCII settings */ }
    public void RenderSummary(string summaryLine) { /* ... */ }
}
