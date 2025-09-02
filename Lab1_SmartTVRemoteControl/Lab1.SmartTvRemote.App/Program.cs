using Lab1.SmartTvRemote.Domain;

var tv = new FakeTv();

// subscribe to events to see real-time output (like attaching a callback in C++)
tv.PowerChanged += (_, on) => Console.WriteLine($"Power: {(on ? "On" : "Off")}");
tv.VolumeChanged += (_, vol) => Console.WriteLine($"Volume: {vol}");
tv.ChannelChanged += (_, ch) => Console.WriteLine($"Channel: {ch}");

await tv.PowerOnAsync();
await tv.SetVolumeAsync(tv.CurrentVolume + 1);
await tv.ChangeChannelAsync(7);
