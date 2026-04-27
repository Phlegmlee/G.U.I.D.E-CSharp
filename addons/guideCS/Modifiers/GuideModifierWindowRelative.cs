using Godot;

namespace GuideCs;

/// <summary>Wrapper for Window Relative Modifier types. Must be assigned a GUIDEModifierWindowRelative to function.</summary>
public partial class GuideModifierWindowRelative : GuideModifier
{
    public GuideModifierWindowRelative(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierWindowRelative()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierWindowRelativeGdPath); }
    
    // Has no unique public calls
}