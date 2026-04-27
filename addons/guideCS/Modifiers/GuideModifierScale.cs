using Godot;

namespace GuideCs;

/// <summary>Wrapper for Scale Modifier types. Must be assigned a GUIDEModifierScale to function.</summary>
public partial class GuideModifierScale : GuideModifier
{
    public GuideModifierScale(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierScale()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierScaleGdPath); }
    
    /// <summary>The scale by which the input should be scaled.</summary>
    public Vector3 Scale
    {
        get => BaseGuideObject.Get("scale").AsVector3();
        set => BaseGuideObject.Set("scale", value);
    }
    
    /// <summary>If true, delta time will be multiplied in addition to the scale.</summary>
    public bool ApplyDeltaTime
    {
        get => BaseGuideObject.Get("apply_delta_time").AsBool();
        set => BaseGuideObject.Set("apply_delta_time", value);
    }
   
}