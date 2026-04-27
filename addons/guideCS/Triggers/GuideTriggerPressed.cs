using Godot;

namespace GuideCs;

/// <summary>Wrapper for Trigger Pressed types. Must be assigned a GUIDETriggerPressed to function.</summary>
public partial class GuideTriggerPressed : GuideTrigger
{
    public GuideTriggerPressed(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerPressed()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerPressedGdPath); }
    
    // Has no unique public calls
    
}