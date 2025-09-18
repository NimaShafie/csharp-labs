// Lab1.SmartTvRemote.Domain/Enums/TvFeatures.cs

namespace Lab1.SmartTvRemote.Domain.Enums
{
    [Flags]
    public enum TvFeatures
    {
        None = 0,
        SmartMenu = 1 << 0,
        HDR = 1 << 1,
        GameMode = 1 << 2,
    }
}
