using Godot;

namespace GuideCs;

/// <summary>Wrapper for Touch Axis 2D Input types. Must be assigned a GUIDEInputTouchAxis2D to function.</summary>
public partial class GuideInputTouchAxis2D : GuideInputTouchAxisBase
{
    public GuideInputTouchAxis2D(GodotObject gdInput) : base(gdInput) { }

    public GuideInputTouchAxis2D()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputTouchAxis2dGdPath); }

    
    // Has no unique public calls
}