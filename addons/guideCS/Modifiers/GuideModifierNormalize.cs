using Godot;

namespace GuideCs;

/// <summary>Wrapper for Normalize Modifier types. Must be assigned a GUIDEModifierNormalize to function.</summary>
public partial class GuideModifierNormalize : GuideModifier
{
    public GuideModifierNormalize(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierNormalize()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierNormalizeGdPath); }
    
    // Has no unique public calls
}