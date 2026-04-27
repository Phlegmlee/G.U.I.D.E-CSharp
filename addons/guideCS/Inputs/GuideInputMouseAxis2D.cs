using Godot;

namespace GuideCs;

/// <summary>Wrapper for Mouse Axis 2D Input types. Must be assigned a GUIDEInputMouseAxis2D to function.</summary>
public partial class GuideInputMouseAxis2D : GuideInput
{
    public GuideInputMouseAxis2D(GodotObject gdInput) : base(gdInput) { }

    public GuideInputMouseAxis2D()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputMouseAxis2dGdPath); }

    // Has no unique public calls
}