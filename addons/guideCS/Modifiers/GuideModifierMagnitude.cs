using Godot;

namespace GuideCs;

/// <summary>Wrapper for Magnitude Modifier types. Must be assigned a GUIDEModifierMagnitude to function.</summary>
public partial class GuideModifierMagnitude : GuideModifier
{
    public GuideModifierMagnitude(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierMagnitude()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierMagnitudeGdPath); }
    
    // Has no unique public calls
}