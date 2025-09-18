namespace Lab1.SmartTvRemote.Domain.Remote
{
    using System;
    using System.Collections.Generic;
    using Lab1.SmartTvRemote.Domain.Contracts;
    using Lab1.SmartTvRemote.Domain.Enums;

    public static class BuiltInCommands
    {
        public static IEnumerable<IRemoteCommand> All()
        {
            // power/volume/channel/mute
            yield return new BasicCommand("Power", "P", "Toggle power", (tv, _) => tv.PowerToggle());
            yield return new BasicCommand("VolUp", "1", "Volume up", (tv, _) => tv.VolumeUp());
            yield return new BasicCommand("VolDown", "2", "Volume down", (tv, _) => tv.VolumeDown());
            yield return new BasicCommand("ChanUp", "3", "Channel up", (tv, _) => tv.ChannelUp());
            yield return new BasicCommand("ChanDown", "4", "Channel down", (tv, _) => tv.ChannelDown());
            yield return new BasicCommand("Mute", "M", "Mute toggle", (tv, _) => tv.MuteToggle());

            // source
            yield return new BasicCommand("Source", "O", "Select source", (tv, _) => tv.OpenSource());
            yield return new BasicCommand("Source0", "0", "Select source", (tv, _) => tv.OpenSource());

            // smart menu
            yield return new FeatureCommand("Smart", "S", "Open Smart Menu", TvFeatures.SmartMenu, (tv, _) => tv.OpenSmartMenu());

            // settings
            yield return new BasicCommand("Settings", "T", "Open Settings", (tv, _) => tv.OpenSettings());

            // direct set channel: setch <N> (bounded 1..1000)
            yield return new BasicCommand("SetChannel", "setch", "Set channel (e.g., 'setch 12')",
                (tv, args) =>
                {
                    if (args.Length > 0 && int.TryParse(args[0], out var ch))
                    {
                        ch = Math.Clamp(ch, 1, 1000);
                        tv.SetChannel(ch);
                    }
                });

            // apps + navigation
            yield return new BasicCommand("Netflix", "N", "Launch Netflix", (tv, _) => tv.OpenApp("Netflix"));
            yield return new BasicCommand("Hulu", "H", "Launch Hulu", (tv, _) => tv.OpenApp("Hulu"));
            yield return new BasicCommand("Prime", "A", "Launch Amazon Prime Video", (tv, _) => tv.OpenApp("Prime Video"));
            yield return new BasicCommand("Return", "R", "Return", (tv, _) => tv.Return());
            yield return new BasicCommand("Exit", "X", "Exit", (tv, _) => tv.Exit());

            // tiny settings shortcuts (optional): 'b +'/'b -', 'c +'/'c -'
            yield return new BasicCommand("Brightness", "b", "Brightness +/- (e.g., 'b +')", (tv, args) => tv.AdjustSetting("brightness", args));
            yield return new BasicCommand("Contrast", "c", "Contrast +/- (e.g., 'c +')", (tv, args) => tv.AdjustSetting("contrast", args));
        }

        private sealed class BasicCommand : IRemoteCommand
        {
            private readonly Action<ISamsungTu7000, string[]> _exec;
            public BasicCommand(string name, string key, string description, Action<ISamsungTu7000, string[]> exec)
            { Name = name; Key = key; Description = description; _exec = exec; }

            public string Name { get; }
            public string Key { get; }
            public string Description { get; }
            public void Execute(ISamsungTu7000 tv, string[] args) => _exec(tv, args);
            public bool IsVisibleFor(ISamsungTu7000 tv) => true;
        }

        private sealed class FeatureCommand : IRemoteCommand
        {
            private readonly TvFeatures _required;
            private readonly Action<ISamsungTu7000, string[]> _exec;
            public FeatureCommand(string name, string key, string description, TvFeatures required, Action<ISamsungTu7000, string[]> exec)
            { Name = name; Key = key; Description = description; _required = required; _exec = exec; }

            public string Name { get; }
            public string Key { get; }
            public string Description { get; }
            public void Execute(ISamsungTu7000 tv, string[] args) => _exec(tv, args);
            public bool IsVisibleFor(ISamsungTu7000 tv) => tv.SupportedFeatures.HasFlag(_required);
        }
    }
}
