using Lab1.SmartTvRemote.Domain.Enums;
using Lab1.SmartTvRemote.Domain.UI;

public sealed class SamsungTu7000_50 : TelevisionBase
{
    public SamsungTu7000_50(Screen screen) : base(screen) { }
    public override string Brand => "Samsung";
    public override string Model => "UN50TU7000";
    public override int SizeInches => 50;
    public override TvFeatures SupportedFeatures =>
        TvFeatures.HDR | TvFeatures.SmartMenu | TvFeatures.GameMode;

    // Optional: override MaxVolume or any behaviors unique to this size
}
