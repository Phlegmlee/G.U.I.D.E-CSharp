using Godot;

namespace GuideCs;

/// <summary>Wrapper for Negate Modifier types. Must be assigned a GUIDEModifierNegate to function.</summary>
public partial class GuideModifierNegate : GuideModifier
{
    public GuideModifierNegate(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierNegate()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierNegateGdPath); }
    
    /// <summary>Whether the X axis should be inverted.</summary>
    public bool X
    {
        get => BaseGuideObject.Get("x").AsBool();
        set => BaseGuideObject.Set("x", value);
    }
    
    /// <summary>Whether the Y axis should be inverted.</summary>
    public bool Y
    {
        get => BaseGuideObject.Get("y").AsBool();
        set => BaseGuideObject.Set("y", value);
    }
    
    /// <summary>Whether the Z axis should be inverted.</summary>
    public bool Z
    {
        get => BaseGuideObject.Get("z").AsBool();
        set => BaseGuideObject.Set("z", value);
    }
   
}