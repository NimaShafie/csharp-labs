using Lab1.SmartTvRemote.Domain.Enums;
using Lab1.SmartTvRemote.Domain.UI;

public sealed class SamsungTu7000_43 : TelevisionBase
{
    public SamsungTu7000_43(Screen screen) : base(screen) { }
    public override string Brand => "Samsung";
    public override string Model => "UN43TU7000";
    public override int SizeInches => 43;
    public override TvFeatures SupportedFeatures =>
        TvFeatures.HDR | TvFeatures.SmartMenu | TvFeatures.GameMode;

    // Optional: override MaxVolume or any behaviors unique to this size
}
