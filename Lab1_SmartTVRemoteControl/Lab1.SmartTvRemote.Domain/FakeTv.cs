using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.SmartTvRemote.Domain
{
    // sealed == C++ 'final' (no subclasses)
    public sealed class FakeTv : ITv, ITvEvents
    {
        // Properties = idiomatic getters in C#
        public bool IsOn { get; private set; }
        public int CurrentVolume { get; private set; } = 10; // like int with default init
        public int CurrentChannel { get; private set; } = 2;

        // C# built-in observer: events (think: callbacks/std::function)
        public event EventHandler<bool>? PowerChanged;
        public event EventHandler<int>?  VolumeChanged;
        public event EventHandler<int>?  ChannelChanged;

        // In a real adapter these would do I/O; here they just mutate memory and notify.
        public Task PowerOnAsync(CancellationToken ct = default)
        {
            if (!IsOn)
            {
                IsOn = true;
                PowerChanged?.Invoke(this, IsOn); // null-conditional: invoke if someone subscribed
            }
            // returning Task.CompletedTask is the async equivalent of "do nothing"
            return Task.CompletedTask;
        }

        public Task PowerOffAsync(CancellationToken ct = default)
        {
            if (IsOn)
            {
                IsOn = false;
                PowerChanged?.Invoke(this, IsOn);
            }
            return Task.CompletedTask;
        }

        public Task SetVolumeAsync(int level, CancellationToken ct = default)
        {
            // Clamp like std::clamp(level, 0, 100)
            var clamped = Math.Min(100, Math.Max(0, level));
            if (clamped != CurrentVolume)
            {
                CurrentVolume = clamped;
                VolumeChanged?.Invoke(this, CurrentVolume);
            }
            return Task.CompletedTask;
        }

        public Task ChangeChannelAsync(int channel, CancellationToken ct = default)
        {
            // Keep it simple: channels >= 1
            var normalized = Math.Max(1, channel);
            if (normalized != CurrentChannel)
            {
                CurrentChannel = normalized;
                ChannelChanged?.Invoke(this, CurrentChannel);
            }
            return Task.CompletedTask;
        }
    }
}
