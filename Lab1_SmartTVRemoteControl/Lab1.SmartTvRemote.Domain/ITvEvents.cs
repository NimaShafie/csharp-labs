using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.SmartTvRemote.Domain
{
    public interface ITvEvents
    {
        event System.EventHandler<bool>? PowerChanged;
        event System.EventHandler<int>? VolumeChanged;
        event System.EventHandler<int>? ChannelChanged;
    }
}
