using Godot;

namespace GuideCs;

/// <summary>Wrapper base for Input Touch Axis class. Does nothing on its own, use with inherited class.</summary>
public partial class GuideInputTouchAxisBase : GuideInputTouchBase
{
    public GuideInputTouchAxisBase(GodotObject gdInput) : base(gdInput) { }

    public GuideInputTouchAxisBase() { }
    
}
    