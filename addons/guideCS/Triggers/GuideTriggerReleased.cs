using Godot;

namespace GuideCs;

/// <summary>Wrapper for Trigger Released types. Must be assigned a GUIDETriggerReleased to function.</summary>
public partial class GuideTriggerReleased : GuideTrigger
{
    public GuideTriggerReleased(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerReleased()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerReleasedGdPath); }
    
    // Has no unique public calls
    
}