using Godot;

namespace GuideCs;

/// <summary>Wrapper for Trigger Tap types. Must be assigned a GUIDETriggerTap to function.</summary>
public partial class GuideTriggerTap : GuideTrigger
{
    public GuideTriggerTap(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerTap()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerTapGdPath); }
    
    /// <summary>The time threshold for the tap to be considered a tap.</summary>
    public float TapThreshold
    {
        get => BaseGuideObject.Get("tap_threshold").AsSingle();
        set => BaseGuideObject.Set("tap_threshold", value);
    }
    
}