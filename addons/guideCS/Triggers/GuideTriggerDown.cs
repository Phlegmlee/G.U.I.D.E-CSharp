using Godot;

namespace GuideCs;

/// <summary>Wrapper for Trigger Down types. Must be assigned a GUIDETriggerDown to function.</summary>
public partial class GuideTriggerDown : GuideTrigger
{
    public GuideTriggerDown(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerDown()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerDownGdPath); }
    
    // Has no unique public calls
    
}