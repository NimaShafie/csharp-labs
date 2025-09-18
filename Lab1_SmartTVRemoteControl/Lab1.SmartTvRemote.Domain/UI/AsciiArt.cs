// Domain/UI/AsciiArt.cs
using System;
using System.Collections.Generic;
using Lab1.SmartTvRemote.Domain.Contracts;

namespace Lab1.SmartTvRemote.Domain.UI
{
    public static class AsciiArt
    {
        private static readonly string[] RemoteWithButtons = new[]
        {
            "  __________________________________     Remote Buttons:                   ",
            " |         TM-1240A Remote          |    [P]    Toggle power               ",
            " |  [P]  Power       [O]  Source    |    [0]    Select source              ",
            " |  [1]  Vol+        [2]  Vol-      |    [1]    Volume up                  ",
            " |  [3]  Ch+         [4]  Ch-       |    [2]    Volume down                ",
            " |  [M]  Mute                       |    [3]    Channel up                 ",
            " |  [S]  Smart       [T]  Settings  |    [4]    Channel down               ",
            " |                                  |    [M]    Mute toggle                ",
            " |                                  |    [S]    Open Smart Menu            ",
            " |  setch <N>                       |    [T]    Open Settings              ",
            " |                                  |    setch  Set channel (1..1000)      ",
            " |                                  |    [N]    Launch Netflix             ",
            " |  [N]  Netflix     [H]  Hulu      |    [H]    Launch Hulu                ",
            " |  [A]  Prime Video                |    [A]    Launch Amazon Prime Video  ",
            " |  [R]  Return      [X]  Exit      |    [R]    Return                     ",
            " |__________________________________|    [X]    Exit                       ",
        };

        public static void PrintRemoteWithButtons(IEnumerable<IRemoteCommand> _ = null!, int __ = 0)
        {
            foreach (var line in RemoteWithButtons)
                Console.WriteLine(line);
        }
    }
}
