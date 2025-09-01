using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.SmartTvRemote.Domain
{
    public interface ITv
    {
        bool IsOn { get; }
        int CurrentVolume { get; }
        int CurrentChannel { get; }

        System.Threading.Tasks.Task PowerOnAsync(System.Threading.CancellationToken ct = default);
        System.Threading.Tasks.Task PowerOffAsync(System.Threading.CancellationToken ct = default);
        System.Threading.Tasks.Task SetVolumeAsync(int level, System.Threading.CancellationToken ct = default);
        System.Threading.Tasks.Task ChangeChannelAsync(int channel, System.Threading.CancellationToken ct = default);
    }
}
