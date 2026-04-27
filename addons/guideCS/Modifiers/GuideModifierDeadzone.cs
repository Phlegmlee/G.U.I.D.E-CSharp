using Godot;

namespace GuideCs;

/// <summary>Wrapper for Deadzone Modifier types. Must be assigned a GUIDEModifierDeadzone to function.</summary>
public partial class GuideModifierDeadzone : GuideModifier
{
    public GuideModifierDeadzone(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierDeadzone()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierDeadzoneGdPath); }
    
    /// <summary>The maximum depth of the ray cast used to detect the 3D position.</summary>
    public float LowerThreshold
    {
        get => BaseGuideObject.Get("lower_threshold").AsSingle();
        set => BaseGuideObject.Set("lower_threshold", value);
    }
    
    /// <summary>The maximum depth of the ray cast used to detect the 3D position.</summary>
    public float UpperThreshold
    {
        get => BaseGuideObject.Get("upper_threshold").AsSingle();
        set => BaseGuideObject.Set("upper_threshold", value);
    }
    
}