[Flags]
public enum TvFeatures
{
    None = 0,
    SmartMenu = 1 << 0,
    HDR = 1 << 1,
    GameMode = 1 << 2,
    // add more if you want to demonstrate feature gating
}

public enum PowerState { Off, On }

public enum InputSource { TV, HDMI1, HDMI2, AV, Apps }
