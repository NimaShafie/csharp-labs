using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.SmartTvRemote.Domain
{
// events, (built-in observer) which raise only when states change
    public interface ITvEvents
    {
        event System.EventHandler<bool>? PowerChanged;
        event System.EventHandler<int>?  VolumeChanged;
        event System.EventHandler<int>?  ChannelChanged;
    }
}
