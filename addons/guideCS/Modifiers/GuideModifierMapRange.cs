using Godot;

namespace GuideCs;

/// <summary>Wrapper for Map Range Modifier types. Must be assigned a GUIDEModifierMapRange to function.</summary>
public partial class GuideModifierMapRange : GuideModifier
{
    public GuideModifierMapRange(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierMapRange()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierMapRangeGdPath); }
    
    /// <summary>Should the output be clamped to the range?</summary>
    public bool ApplyClamp
    {
        get => BaseGuideObject.Get("apply_clamp").AsBool();
        set => BaseGuideObject.Set("apply_clamp", value);
    }
    
    /// <summary>The minimum input value.</summary>
    public float InputMin
    {
        get => BaseGuideObject.Get("input_min").AsSingle();
        set => BaseGuideObject.Set("input_min", value);
    }
    
    /// <summary>The maximum input value.</summary>
    public float InputMax
    {
        get => BaseGuideObject.Get("input_max").AsSingle();
        set => BaseGuideObject.Set("input_max", value);
    }
    
    /// <summary>The minimum output value.</summary>
    public float OutputMin
    {
        get => BaseGuideObject.Get("output_min").AsSingle();
        set => BaseGuideObject.Set("output_min", value);
    }
    
    /// <summary>The maximum output value.</summary>
    public float OutputMax
    {
        get => BaseGuideObject.Get("output_max").AsSingle();
        set => BaseGuideObject.Set("output_max", value);
    }
    
    /// <summary>Apply modifier to X axis?</summary>
    public bool X
    {
        get => BaseGuideObject.Get("x").AsBool();
        set => BaseGuideObject.Set("x", value);
    }
    
    /// <summary>Apply modifier to Y axis?</summary>
    public bool Y
    {
        get => BaseGuideObject.Get("y").AsBool();
        set => BaseGuideObject.Set("y", value);
    }
    
    /// <summary>Apply modifier to Z axis?</summary>
    public bool Z
    {
        get => BaseGuideObject.Get("z").AsBool();
        set => BaseGuideObject.Set("z", value);
    }
   
}