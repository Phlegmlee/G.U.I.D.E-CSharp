using Godot;

namespace GuideCs;

/// <summary>Wrapper for Hair Trigger types. Must be assigned a GUIDETriggerHair to function.</summary>
public partial class GuideTriggerHair : GuideTrigger
{
    public GuideTriggerHair(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerHair()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerHairGdPath); }
    
    // Has no unique public calls
}