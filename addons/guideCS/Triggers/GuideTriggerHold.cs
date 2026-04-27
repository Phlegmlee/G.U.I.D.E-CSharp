using Godot;

namespace GuideCs;

/// <summary>Wrapper for Trigger Hold types. Must be assigned a GUIDETriggerHold to function.</summary>
public partial class GuideTriggerHold : GuideTrigger
{
    public GuideTriggerHold(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerHold()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerHoldGdPath); }
    
    /// <summary>The time for how long the input must be held.</summary>
    public float HoldThreshold
    {
        get => BaseGuideObject.Get("hold_treshold").AsSingle();
        set => BaseGuideObject.Set("hold_treshold", value);
    }
    
    /// <summary>If true, the trigger will only fire once until the input is released. Otherwise, the trigger will fire
    /// every frame.</summary>
    public bool IsOneShot
    {
        get => BaseGuideObject.Get("is_one_shot").AsBool();
        set => BaseGuideObject.Set("is_one_shot", value);
    }
    
}