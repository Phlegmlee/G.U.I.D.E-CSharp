using Godot;

namespace GuideCs;

/// <summary>Wrapper for Touch Position Input types. Must be assigned a GUIDEInputTouchPosition to function.</summary>
public partial class GuideInputTouchPosition : GuideInputTouchBase
{
    public GuideInputTouchPosition(GodotObject gdInput) : base(gdInput) { }

    public GuideInputTouchPosition()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputTouchPositionGdPath); }

    // Has no unique public calls
}