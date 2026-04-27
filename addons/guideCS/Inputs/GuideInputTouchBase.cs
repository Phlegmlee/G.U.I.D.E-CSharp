using Godot;

namespace GuideCs;

/// <summary>Wrapper base for Input Touch classes. Does nothing on its own, use with inherited class.</summary>
public partial class GuideInputTouchBase : GuideInput
{
    public GuideInputTouchBase(GodotObject gdInput) : base(gdInput) { }

    public GuideInputTouchBase() { }
    
    /// <summary>The number of fingers to be tracked.</summary>
    public int FingerCount
    {
        get => BaseGuideObject.Get("finger_count").AsInt32();
        set => BaseGuideObject.Set("finger_count", value);
    }
    
    /// <summary>The index of the finger for which the position/delta should be reported  (0 = first finger, 1 = second
    /// finger, etc.). If -1, reports the average position/delta for all fingers currently touching.</summary>
    public int FingerIndex
    {
        get => BaseGuideObject.Get("finger_index").AsInt32();
        set => BaseGuideObject.Set("finger_index", value);
    }
}