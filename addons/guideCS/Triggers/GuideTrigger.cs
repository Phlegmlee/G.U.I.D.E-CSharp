using Godot;

namespace GuideCs;

/// <summary>Wrapper for Triggers. Must be assigned a GUIDETrigger to function.</summary>
public partial class GuideTrigger : GuideResource
{
    public GuideTrigger(GodotObject gdGuideTrigger) : base(gdGuideTrigger) { }
    
    public GuideTrigger()
    { LoadAndConnectBaseGuideResource($"{ResourceLibrary.TriggerGdPath}"); }

    
    public float ActuationThreshold
    {
        get => BaseGuideObject.Get("actuation_threshold").AsSingle();
        set => BaseGuideObject.Set("actuation_threshold", value);
    }

    /// <summary>Returns whether this trigger is the same as the 'other' trigger. This is used to determine if a trigger
    /// can be reused during context switching.</summary>
    /// <returns>True if they are the same.</returns>
    public bool IsSameAs(GuideTrigger other)
    {
        return BaseGuideObject.Call("is_same_as", other.BaseGuideObject).AsBool();
    }
}