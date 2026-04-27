using Godot;

namespace GuideCs;

/// <summary>Wrapper for Canvas Coordinate Modifier types. Must be assigned a GUIDEModifierCanvasCoordinates to function.</summary>
public partial class GuideModifierCanvasCoordinates : GuideModifier
{
    public GuideModifierCanvasCoordinates(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierCanvasCoordinates()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierCanvasCoordinatesGdPath); }
    
    /// <summary>If True, the input will be treated as relative input (position change) rather than absolute input (position).</summary>
    public bool RelativeInput
    {
        get => BaseGuideObject.Get("relative_input").AsBool();
        set => BaseGuideObject.Set("relative_input", value);
    }
    
}