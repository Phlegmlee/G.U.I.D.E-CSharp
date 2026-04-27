using Godot;

namespace GuideCs;

/// <summary>Wrapper for Curve Modifier types. Must be assigned a GUIDEModifierCurve to function.</summary>
public partial class GuideModifierCurve : GuideModifier
{
    #region StaticLoader

    private static readonly GDScript ModifierCurveStatic;
    static GuideModifierCurve()
    { ModifierCurveStatic = ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierCurveGdPath); }
    
    #endregion StaticLoader
    
    public GuideModifierCurve(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierCurve()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierCurveGdPath); }
    
    /// <summary>The curve to apply to the x-axis.</summary>
    public Curve Curve
    {
        get => BaseGuideObject.Get("curve").As<Curve>();
        set => BaseGuideObject.Set("curve", value);
    }
    
    /// <summary>Apply modifier to X axis.</summary>
    public bool X
    {
        get => BaseGuideObject.Get("x").AsBool();
        set => BaseGuideObject.Set("x", value);
    }
    
    /// <summary>Apply modifier to Y axis.</summary>
    public bool Y
    {
        get => BaseGuideObject.Get("y").AsBool();
        set => BaseGuideObject.Set("y", value);
    }
    
    /// <summary>Apply modifier to Z axis.</summary>
    public bool Z
    {
        get => BaseGuideObject.Get("z").AsBool();
        set => BaseGuideObject.Set("z", value);
    }
    
    /// <summary>Create and return a default curve resource with a smoothstep, 0.0 - 1.0 input/output range.</summary>
    public static Curve DefaultCurve() => ModifierCurveStatic.Call("default_curve").As<Curve>();
}